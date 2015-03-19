using System;
using System.Text.RegularExpressions;
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
        }

        private async Task ScanBlockchain()
        {
            var transactions = await _nxtService.ScanBlockchain(_blockchainStatus.LastSecureBlockId.ToUnsigned());
            foreach (var transaction in transactions)
            {
                await ProcessTransaction(transaction);
            }
        }

        private async Task ProcessTransaction(InboundTransaction transaction)
        {
            var dbTransaction = await _repository.GetInboundTransactionAsync(transaction.TransactionId);
            var accountRegex = new Regex("account:\\s?([\\d]+)", RegexOptions.IgnoreCase);
            var match = accountRegex.Match(transaction.DecryptedMessage);
            if (match.Success)
            {
                transaction.CustomerId = Convert.ToInt32(match.Groups[1].Value);
            }
            if (dbTransaction == null)
            {
                await _repository.AddInboundTransactionAsync(transaction);
                OnIncomingTransaction(new IncomingTransactionEventArgs(transaction));
            }
            else if (dbTransaction.Status != transaction.Status)
            {
                await _repository.UpdateTransactionStatusAsync(transaction.TransactionId, transaction.Status);
                OnUpdatedTransactionStatus(new StatusUpdatedEventArgs(transaction, dbTransaction.Status));
            }
        }

        private async Task Init()
        {
            await _nxtService.Init();
            _blockchainStatus = await _repository.GetBlockchainStatusAsync();
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
