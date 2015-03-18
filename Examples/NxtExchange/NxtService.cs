using System;
using System.Threading.Tasks;
using NxtLib;
using NxtLib.Accounts;
using NxtLib.Blocks;

namespace NxtExchange
{
    public interface INxtService
    {
        event EventHandler IncomingTransaction;
        Task Init();
        Task ScanBlockchain(ulong lastConfirmedBlockId);
    }

    public class NxtService : INxtService
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly string _secretPhrase;
        private string _accountRs;
        private string _publicKey;

        public event EventHandler IncomingTransaction;

        public NxtService(string secretPhrase, IAccountService accountService, IBlockService blockService)
        {
            _secretPhrase = secretPhrase;
            _accountService = accountService;
            _blockService = blockService;
        }

        public async Task Init()
        {
            var accountIdReply = await _accountService.GetAccountId(AccountIdLocator.BySecretPhrase(_secretPhrase));
            _accountRs = accountIdReply.AccountRs;
            _publicKey = accountIdReply.PublicKey;
        }

        public async Task ScanBlockchain(ulong lastConfirmedBlockId)
        {
            var block = await _blockService.GetBlock(BlockLocator.BlockId(lastConfirmedBlockId));
            var accountTransactionsReply = await _accountService.GetAccountTransactions(_accountRs, 
                block.Timestamp.AddSeconds(1), TransactionSubType.PaymentOrdinaryPayment);

            accountTransactionsReply.Transactions.ForEach(HandleTransaction);
        }

        private void HandleTransaction(Transaction transaction)
        {
            Raise(IncomingTransaction, EventArgs.Empty);
        }

        private void Raise(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
