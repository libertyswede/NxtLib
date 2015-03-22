using System;
using System.Linq;
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

        private async Task Init()
        {
            await _nxtService.Init();
            _blockchainStatus = await _repository.GetBlockchainStatus();
        }

        /// <summary>
        /// Scans the blockchain from last known secure block id until last block
        /// </summary>
        private async Task ScanBlockchain()
        {
            // Fetch new blockchain status before we start processing transactions
            var newBlockchainStatus = await _nxtService.GetBlockchainStatus();

            // Fetch transactions from blockchain and process them
            var transactions = await _nxtService.CheckForTransactions(_blockchainStatus.LastSecureBlockTimestamp.AddSeconds(1));
            transactions.ForEach(async t => await ProcessTransaction(t));

            // Look for previously recorded transactions in db that has been orphaned
            var dbTransactions = await _repository.GetNonSecuredTransactions();
            foreach (var dbTransaction in dbTransactions.Where(dbTransaction => transactions.All(t => t.TransactionId != dbTransaction.TransactionId)))
            {
                await UpdatedTransaction(dbTransaction, TransactionStatus.Removed);
            }

            await UpdateBlockchainStatus(newBlockchainStatus);
        }

        private async Task ListenForTransactions()
        {
            while (true)
            {
                var newBlockchainStatus = await _nxtService.GetBlockchainStatus();
                var confirmedTransactions = await _nxtService.CheckForTransactions(_blockchainStatus.LastSecureBlockTimestamp.AddSeconds(1), 0);
                var dbTransactions = await _repository.GetNonSecuredTransactions();

                confirmedTransactions.ForEach(t => ProcessTransaction(t));

                // unprocessed means the transaction either became secured or orphaned (removed)
                var unprocessedDbTransactions = dbTransactions.Where(dbTransaction => 
                    confirmedTransactions.All(t => t.TransactionId != dbTransaction.TransactionId))
                    .ToList();

                foreach (var unprocessedDbTransaction in unprocessedDbTransactions)
                {
                    var transaction = await _nxtService.GetTransaction(unprocessedDbTransaction.TransactionId.ToUnsigned());
                    if (transaction != null)
                    {
                        var status = TransactionStatusCalculator.GetStatus(transaction.Confirmations);
                        if (unprocessedDbTransaction.Status != status)
                        {
                            await UpdatedTransaction(unprocessedDbTransaction, status);
                        }
                    }
                    else
                    {
                        await UpdatedTransaction(unprocessedDbTransaction, TransactionStatus.Removed);
                    }
                }

                await UpdateBlockchainStatus(newBlockchainStatus);

                await Task.Delay(new TimeSpan(0, 0, 10));
            }
        }

        private async Task ProcessTransaction(InboundTransaction transaction)
        {
            var dbTransaction = await _repository.GetInboundTransaction(transaction.TransactionId);
            if (dbTransaction == null)
            {
                await NewTransaction(transaction);
            }
            else if (dbTransaction.Status != transaction.Status)
            {
                await UpdatedTransaction(dbTransaction, transaction.Status);
            }
        }

        private async Task NewTransaction(InboundTransaction transaction)
        {
            await _repository.AddInboundTransaction(transaction);
            OnIncomingTransaction(new IncomingTransactionEventArgs(transaction));
        }

        private async Task UpdatedTransaction(InboundTransaction transaction, TransactionStatus status)
        {
            await _repository.UpdateTransactionStatus(transaction.TransactionId, status);
            OnUpdatedTransactionStatus(new StatusUpdatedEventArgs(transaction, status));
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

        private async Task UpdateBlockchainStatus(BlockchainStatus newBlockchainStatus)
        {
            _blockchainStatus = newBlockchainStatus;
            await _repository.UpdateBlockchainStatus(_blockchainStatus);
        }
    }
}
