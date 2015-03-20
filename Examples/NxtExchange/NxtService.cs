using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtExchange.DAL;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;
using NxtLib.ServerInfo;
using NxtLib.Transactions;
using TransactionSubType = NxtLib.TransactionSubType;

namespace NxtExchange
{
    public interface INxtService
    {
        Task Init();
        Task<List<InboundTransaction>> CheckForTransactions(DateTime blockDateTime, int? numberOfConfirmations = null);
        Task<BlockchainStatus> GetBlockchainStatus();
        Task<string> DecryptMessage(Transaction transaction);
        Task<Transaction> GetTransaction(ulong transactionId);
    }

    public class NxtService : INxtService
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly IMessageService _messageService;
        private readonly IServerInfoService _serverInfoService;
        private readonly ITransactionService _transactionService;
        private readonly string _secretPhrase;
        private string _accountRs;

        public NxtService(string secretPhrase, IAccountService accountService, IBlockService blockService,
            IMessageService messageService, IServerInfoService serverInfoService, ITransactionService transactionService)
        {
            _secretPhrase = secretPhrase;
            _accountService = accountService;
            _blockService = blockService;
            _messageService = messageService;
            _serverInfoService = serverInfoService;
            _transactionService = transactionService;
        }

        public async Task Init()
        {
            var accountIdReply = await _accountService.GetAccountId(AccountIdLocator.BySecretPhrase(_secretPhrase));
            _accountRs = accountIdReply.AccountRs;
        }

        public async Task<BlockchainStatus> GetBlockchainStatus()
        {
            var status = new BlockchainStatus();

            var blockchainStatusReply = await _serverInfoService.GetBlockchainStatus();
            var lastBlock = await _blockService.GetBlock(BlockLocator.BlockId(blockchainStatusReply.LastBlockId));
            var confirmedBlock = await _blockService.GetBlock(BlockLocator.Height(blockchainStatusReply.NumberOfBlocks - 11));
            var secureBlock = await _blockService.GetBlock(BlockLocator.Height(blockchainStatusReply.NumberOfBlocks - 721));

            status.LastKnownBlockId = blockchainStatusReply.LastBlockId.ToSigned();
            status.LastKnownBlockTimestamp = lastBlock.Timestamp;

            status.LastConfirmedBlockId = confirmedBlock.BlockId.ToSigned();
            status.LastConfirmedBlockTimestamp = confirmedBlock.Timestamp;

            status.LastSecureBlockId = secureBlock.BlockId.ToSigned();
            status.LastSecureBlockTimestamp = secureBlock.Timestamp;

            return status;
        }

        public async Task<List<InboundTransaction>> CheckForTransactions(DateTime blockDateTime, int? numberOfConfirmations = null)
        {
            var recievedTransactions = new List<InboundTransaction>();
            var accountTransactions = await _accountService.GetAccountTransactions(_accountRs,
                blockDateTime, TransactionSubType.PaymentOrdinaryPayment, numberOfConfirmations: numberOfConfirmations);

            foreach (var transaction in accountTransactions.Transactions.Where(t => t.RecipientRs.Equals(_accountRs)))
            {
                var inboundTransaction = new InboundTransaction(transaction)
                {
                    DecryptedMessage = await DecryptMessage(transaction)
                };
                recievedTransactions.Add(inboundTransaction);
            }

            return recievedTransactions;
        }

        public async Task<string> DecryptMessage(Transaction transaction)
        {
            if (transaction.EncryptedMessage == null)
            {
                return string.Empty;
            }
            var decryptedMessage = await _messageService.DecryptMessageFrom(transaction.SenderRs, transaction.EncryptedMessage, _secretPhrase);
            return decryptedMessage.Message;
        }

        public async Task<Transaction> GetTransaction(ulong transactionId)
        {
            try
            {
                var transactionReply = await _transactionService.GetTransaction(new GetTransactionLocator(transactionId));
                return (Transaction) transactionReply;
            }
            catch (NxtException)
            {
                return null;
            }
        }
    }
}
