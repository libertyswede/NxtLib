using System.Collections.Generic;
using System.Threading.Tasks;
using NxtExchange.DAL;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;

namespace NxtExchange
{
    public interface INxtService
    {
        Task Init();
        Task<List<InboundTransaction>> ScanBlockchain(ulong lastSecureBlockId);
    }

    public class NxtService : INxtService
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly IMessageService _messageService;
        private readonly string _secretPhrase;
        private string _accountRs;
        private string _publicKey;

        public NxtService(string secretPhrase, IAccountService accountService, IBlockService blockService, IMessageService messageService)
        {
            _secretPhrase = secretPhrase;
            _accountService = accountService;
            _blockService = blockService;
            _messageService = messageService;
        }

        public async Task Init()
        {
            var accountIdReply = await _accountService.GetAccountId(AccountIdLocator.BySecretPhrase(_secretPhrase));
            _accountRs = accountIdReply.AccountRs;
            _publicKey = accountIdReply.PublicKey;
        }

        public async Task<List<InboundTransaction>> ScanBlockchain(ulong lastSecureBlockId)
        {
            var recievedTransactions = new List<InboundTransaction>();
            var block = await _blockService.GetBlock(BlockLocator.BlockId(lastSecureBlockId));
            var accountTransactionsReply = await _accountService.GetAccountTransactions(_accountRs, 
                block.Timestamp.AddSeconds(1), TransactionSubType.PaymentOrdinaryPayment);

            foreach (var transaction in accountTransactionsReply.Transactions)
            {
                var inboundTransaction = new InboundTransaction(transaction)
                {
                    DecryptedMessage = await DecryptMessage(transaction)
                };
                recievedTransactions.Add(inboundTransaction);
            }

            return recievedTransactions;
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
