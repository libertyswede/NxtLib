using System.Collections.Generic;
using System.Threading.Tasks;
using NxtExchange.DAL;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;
using NxtLib.ServerInfo;
using TransactionSubType = NxtLib.TransactionSubType;

namespace NxtExchange
{
    public interface INxtService
    {
        Task Init();
        Task<List<InboundTransaction>> ScanBlockchain(ulong lastSecureBlockId);
        Task<List<Transaction>> CheckForTransactions(int firstIndex, int lastIndex);
        Task<BlockchainStatus> GetBlockchainStatus();
    }

    public class NxtService : INxtService
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly IMessageService _messageService;
        private readonly IServerInfoService _serverInfoService;
        private readonly string _secretPhrase;
        private string _accountRs;

        public NxtService(string secretPhrase, IAccountService accountService, IBlockService blockService,
            IMessageService messageService, IServerInfoService serverInfoService)
        {
            _secretPhrase = secretPhrase;
            _accountService = accountService;
            _blockService = blockService;
            _messageService = messageService;
            _serverInfoService = serverInfoService;
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
            var secureBlock = await _blockService.GetBlock(BlockLocator.Height(blockchainStatusReply.NumberOfBlocks - 721));

            status.LastKnownBlockId = blockchainStatusReply.LastBlockId.ToSigned();
            status.LastKnownBlockHeight = blockchainStatusReply.NumberOfBlocks - 1;
            status.LastSecureBlockId = secureBlock.BlockId.ToSigned();
            status.LastSecureBlockHeight = secureBlock.Height;

            return status;
        }

        public async Task<List<InboundTransaction>> ScanBlockchain(ulong lastSecureBlockId)
        {
            var recievedTransactions = new List<InboundTransaction>();
            var block = await _blockService.GetBlock(BlockLocator.BlockId(lastSecureBlockId));
            var accountTransactions = await _accountService.GetAccountTransactions(_accountRs, 
                block.Timestamp.AddSeconds(1), TransactionSubType.PaymentOrdinaryPayment);

            foreach (var transaction in accountTransactions.Transactions)
            {
                var inboundTransaction = new InboundTransaction(transaction)
                {
                    DecryptedMessage = await DecryptMessage(transaction)
                };
                recievedTransactions.Add(inboundTransaction);
            }

            return recievedTransactions;
        }

        public async Task<List<Transaction>> CheckForTransactions(int firstIndex, int lastIndex)
        {
            //var service = new NxtLib.ServerInfo.ServerInfoService();
            //var blocks = await _blockService.GetBlocks(0, 1);
            //blocks.BlockList.First().

            var accountTransactions = await _accountService.GetAccountTransactions(_accountRs, 
                transactionType: TransactionSubType.PaymentOrdinaryPayment, firstIndex: firstIndex, lastIndex: lastIndex);
            
            return accountTransactions.Transactions;
        }

        private async Task<string> DecryptMessage(Transaction transaction)
        {
            if (transaction.EncryptedMessage == null)
            {
                return string.Empty;
            }
            var decryptedMessage = await _messageService.DecryptMessageFrom(transaction.SenderRs, transaction.EncryptedMessage, _secretPhrase);
            return decryptedMessage.Message;
        }
    }
}
