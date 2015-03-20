using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NxtExchange.DAL;

namespace NxtExchange
{
    public class ExchangeController
    {
        public event IncomingTransactionHandler IncomingTransaction;
        public event TransactionStatusUpdatedHandler UpdatedTransactionStatus;

        private readonly INxtService _nxtService;
        private readonly INxtRepository _repository;
        private BlockchainStatus _blockchainStatus;

        public ExchangeController(INxtService nxtService, INxtRepository repository)
        {
            _nxtService = nxtService;
            _repository = repository;
        }

        public async Task Start()
        {
            await Init();
            await ScanBlockchain();
            await ListenForTransactions();
        }

        private async Task ListenForTransactions()
        {
            while (true)
            {
                var index = 0;
                const int page = 10;
                var hasMore = true;
                while (hasMore)
                {
                    var transactions = await _nxtService.CheckForTransactions(index, page);
                    var transactionIds = transactions.Select(t => t.TransactionId.Value.ToSigned());
                    var inboundTransactions = await _repository.GetInboundTransactions(transactionIds);
                    index += page;
                }

                await Task.Delay(new TimeSpan(0, 0, 10));
            }
        }

        private async Task ScanBlockchain()
        {
            var newBlockchainStatus = await _nxtService.GetBlockchainStatus();
            var transactions = await _nxtService.ScanBlockchain(_blockchainStatus.LastSecureBlockId.ToUnsigned());
            foreach (var transaction in transactions)
            {
                await ProcessTransaction(transaction);
            }
            await UpdateBlockchainStatus(newBlockchainStatus);
        }

        private async Task UpdateBlockchainStatus(BlockchainStatus newBlockchainStatus)
        {
            _blockchainStatus = newBlockchainStatus;
            await _repository.UpdateBlockchainStatus(_blockchainStatus);
        }

        private async Task ProcessTransaction(InboundTransaction transaction)
        {
            var dbTransaction = await _repository.GetInboundTransaction(transaction.TransactionId);
            var accountRegex = new Regex("account:\\s?([\\d]+)", RegexOptions.IgnoreCase);
            var match = accountRegex.Match(transaction.DecryptedMessage);
            if (match.Success)
            {
                transaction.CustomerId = Convert.ToInt32(match.Groups[1].Value);
            }
            if (dbTransaction == null)
            {
                await _repository.AddInboundTransaction(transaction);
                OnIncomingTransaction(new IncomingTransactionEventArgs(transaction));
            }
            else if (dbTransaction.Status != transaction.Status)
            {
                await _repository.UpdateTransactionStatus(transaction.TransactionId, transaction.Status);
                OnUpdatedTransactionStatus(new StatusUpdatedEventArgs(transaction, dbTransaction.Status));
            }
        }

        private async Task Init()
        {
            await _nxtService.Init();
            _blockchainStatus = await _repository.GetBlockchainStatus();
        }

        private void OnIncomingTransaction(IncomingTransactionEventArgs e)
        {
            var handler = IncomingTransaction;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnUpdatedTransactionStatus(StatusUpdatedEventArgs e)
        {
            var handler = UpdatedTransactionStatus;
            if (handler != null) handler(this, e);
        }
    }
}
