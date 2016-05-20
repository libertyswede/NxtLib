using System;
using System.Collections.Generic;
using System.Linq;
using NxtLib.Local;
using NxtLib.Shuffling;
using NxtLib;
using NxtLib.Transactions;

namespace FailedShufflings
{
    class Program
    {
        static ITransactionService transactionService;

        static void Main(string[] args)
        {
            IServiceFactory serviceFactory = new ServiceFactory(Constants.DefaultNxtUrl);
            transactionService = serviceFactory.CreateTransactionService();
            var shufflingService = serviceFactory.CreateShufflingService();
            var serverInfoService = serviceFactory.CreateServerInfoService();

            var state = serverInfoService.GetState().Result;
            var shufflingDeadline = (state.IsTestnet ? 10 : 100) - 1;
            var shufflingDepositNxt = state.IsTestnet ? 7 : 1000;

            var allShufflings = shufflingService.GetAllShufflings(true, false).Result;

            foreach (var shuffling in allShufflings.Shufflings.Where(s => ShufflingFailed(s)))
            {
                var shufflingTransaction = transactionService.GetTransaction(GetTransactionLocator.ByTransactionId(shuffling.ShufflingId)).Result;
                var shufflingParticipants = shufflingService.GetShufflingParticipants(shuffling.ShufflingId).Result;
                var priorShufflingStage = GetPriorShufflingStage(shufflingParticipants.Participants);
                var blameAccountRs = shuffling.AssigneeRs;
                int height = 0;
                Transaction transaction = null;

                if (priorShufflingStage == ShufflingStage.Processing)
                {
                    if (shufflingParticipants.Participants.All(p => p.State == ShufflingParticipantState.Registered))
                    {
                        var lastAccountRs = GetLastAccountRs(shufflingParticipants);
                        transaction = GetShufflingRegistrationTransaction(shufflingTransaction, lastAccountRs);
                    }
                    else // at least 1 participant processed
                    {
                        var previousAccountRs = GetPreviousAccountRs(shufflingParticipants, blameAccountRs);
                        transaction = GetShufflingProcessingTransaction(shuffling, shufflingTransaction, previousAccountRs);
                    }
                    height = transaction.Height + shufflingDeadline;
                }
                else // priorShufflingStage == ShufflingStage.Verification
                {
                    if (shufflingParticipants.Participants.All(p => p.State == ShufflingParticipantState.Processed))
                    {
                        var lastAccountRs = GetLastAccountRs(shufflingParticipants);
                        transaction = GetShufflingProcessingTransaction(shuffling, shufflingTransaction, lastAccountRs);
                    }
                    else // at least 1 participant verified
                    {
                        var previousAccountRs = GetPreviousAccountRs(shufflingParticipants, blameAccountRs);
                        transaction = GetShufflingVerificationTransaction(shuffling, shufflingTransaction, previousAccountRs);
                    }
                    height = transaction.Height + shufflingDeadline + shuffling.ParticipantCount;
                }

                var refund = shuffling.Amount.Nqt - Amount.CreateAmountFromNxt(shufflingDepositNxt).Nqt;
                var refundNxt = Amount.CreateAmountFromNqt(refund).Nxt;
                Console.WriteLine($"{blameAccountRs} is to blame for shuffling {shuffling.ShufflingId} @ height {height} and only gets back {refundNxt} (from {shuffling.Amount.Nxt}) NXT.");
            }
            Console.WriteLine("Done and done!");
            Console.ReadLine();
        }

        private static bool ShufflingFailed(ShufflingData shuffling)
        {
            return shuffling.Stage == ShufflingStage.Cancelled && shuffling.ParticipantCount == shuffling.RegistrantCount;
        }

        private static Transaction GetShufflingVerificationTransaction(ShufflingData shuffling, TransactionReply shufflingTransaction, ShufflingParticipant previousAccountRs)
        {
            var verifyTransactions = transactionService.GetBlockchainTransactions(previousAccountRs.AccountId, shufflingTransaction.BlockTimestamp, TransactionSubType.ShufflingVerification).Result;
            var verifyTransaction = verifyTransactions.Transactions.Single(t => ((ShufflingVerificationAttachment)t.Attachment).ShufflingId == shuffling.ShufflingId);
            return verifyTransaction;
        }

        private static Transaction GetShufflingProcessingTransaction(ShufflingData shuffling, TransactionReply shufflingTransaction, ShufflingParticipant previousAccountRs)
        {
            var processTransactions = transactionService.GetBlockchainTransactions(previousAccountRs.AccountId, shufflingTransaction.BlockTimestamp, TransactionSubType.ShufflingProcessing).Result;
            var processTransaction = processTransactions.Transactions.Single(t => ((ShufflingProcessingAttachment)t.Attachment).ShufflingId == shuffling.ShufflingId);
            return processTransaction;
        }

        private static Transaction GetShufflingRegistrationTransaction(TransactionReply shufflingTransaction, ShufflingParticipant lastAccountRs)
        {
            var registerTransactions = transactionService.GetBlockchainTransactions(lastAccountRs.AccountId, shufflingTransaction.BlockTimestamp, TransactionSubType.ShufflingRegistration).Result;
            var registerTransaction = registerTransactions.Transactions.Single(t => ((ShufflingRegistrationAttachment)t.Attachment).ShufflingFullHash.Equals(shufflingTransaction.FullHash));
            return registerTransaction;
        }

        private static ShufflingParticipant GetLastAccountRs(ShufflingParticipantsReply shufflingParticipants)
        {
            return shufflingParticipants.Participants.Single(p => p.NextAccountId == 0);
        }

        private static ShufflingParticipant GetPreviousAccountRs(ShufflingParticipantsReply shufflingParticipants, string blameAccountRs)
        {
            return shufflingParticipants.Participants.Single(p => p.NextAccountRs == blameAccountRs);
        }

        private static ShufflingStage GetPriorShufflingStage(IList<ShufflingParticipant> participants)
        {
            var maxParticipantState = (ShufflingParticipantState)participants.ToList().Select(p => (int)p.State).Max();

            if (maxParticipantState == ShufflingParticipantState.Registered)
            {
                return ShufflingStage.Processing;
            }
            if (maxParticipantState == ShufflingParticipantState.Processed)
            {
                if (!participants.All(p => p.State == ShufflingParticipantState.Processed))
                {
                    return ShufflingStage.Processing;
                }
                else
                {
                    return ShufflingStage.Verification;
                }
            }
            if (maxParticipantState == ShufflingParticipantState.Verified)
            {
                return ShufflingStage.Verification;
            }
            throw new NotSupportedException();
        }
    }
}
