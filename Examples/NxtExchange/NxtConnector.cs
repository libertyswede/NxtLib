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
using BlockchainStatus = NxtExchange.DAL.BlockchainStatus;
using TransactionSubType = NxtLib.TransactionSubType;

namespace NxtExchange
{
    public interface INxtConnector
    {
        Task Init();
        Task<List<InboundTransaction>> CheckForTransactions(DateTime blockDateTime, int? numberOfConfirmations = null);
        Task<BlockchainStatus> GetBlockchainStatus();
        Task<string> DecryptMessage(Transaction transaction);
        Task<Transaction> GetTransaction(ulong transactionId);
    }

    public class NxtConnector : INxtConnector
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly IMessageService _messageService;
        private readonly IServerInfoService _serverInfoService;
        private readonly ITransactionService _transactionService;
        private readonly string _secretPhrase;
        private ulong _accountId;

        public NxtConnector(string secretPhrase, IAccountService accountService, IBlockService blockService,
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
            _accountId = accountIdReply.AccountId;
        }

        public async Task<BlockchainStatus> GetBlockchainStatus()
        {
            var status = new BlockchainStatus();

            var blockchainStatusReply = await _serverInfoService.GetBlockchainStatus();
            var lastBlock = await _blockService.GetBlock(BlockLocator.ByBlockId(blockchainStatusReply.LastBlockId));
            var confirmedBlock = await _blockService.GetBlock(BlockLocator.ByHeight(blockchainStatusReply.NumberOfBlocks - 11));
            var secureBlock = await _blockService.GetBlock(BlockLocator.ByHeight(blockchainStatusReply.NumberOfBlocks - 721));

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
            var accountTransactions = await _transactionService.GetBlockchainTransactions(_accountId.ToString(), blockDateTime, TransactionSubType.PaymentOrdinaryPayment, numberOfConfirmations: numberOfConfirmations);
            foreach (var transaction in accountTransactions.Transactions.Where(t => t.Recipient == _accountId).OrderBy(t => t.BlockTimestamp).ThenBy(t => t.Timestamp))
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
            var decryptedMessage = await _messageService.ReadMessage(transaction.TransactionId.Value, _secretPhrase);
            return decryptedMessage.DecryptedMessage.MessageText;
        }

        public async Task<Transaction> GetTransaction(ulong transactionId)
        {
            try
            {
                var transactionReply = await _transactionService.GetTransaction(GetTransactionLocator.ByTransactionId(transactionId));
                return (Transaction) transactionReply;
            }
            catch (NxtException)
            {
                return null;
            }
        }
    }
}
