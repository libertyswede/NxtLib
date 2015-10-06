using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Transactions
{
    public interface ITransactionService
    {
        Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameter parameter,
            string prunableAttachmentJson = null);

        Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString unsignedTransactionBytes,
            BinaryHexString signatureHash);

        Task<TransactionListReply> GetBlockchainTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null, bool? includeExpiredPrunable = null, bool? includePhasingResult = null,
            bool? executedOnly = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ExpectedTransactionsReply> GetExpectedTransactions(IEnumerable<string> accountIds = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionReply> GetTransaction(GetTransactionLocator locator, bool? includePhasingResult = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(IEnumerable<string> accountIds = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(IEnumerable<string> accountIds = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter,
            string prunableAttachmentJson = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, string secretPhrase,
            string prunableAttachmentJson = null, bool? validate = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);
    }
}