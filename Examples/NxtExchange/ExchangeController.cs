using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NxtExchange.DAL;

namespace NxtExchange
{
    public class ExchangeController
    {
        public event IncomingTransactionHandler IncomingTransaction;
        public event TransactionStatusUpdatedHandler UpdatedTransactionStatus;

        private readonly INxtConnector _nxtConnector;
        private readonly INxtRepository _repository;
        private BlockchainStatus _blockchainStatus;

        public ExchangeController(INxtConnector nxtConnector, INxtRepository repository)
        {
            _nxtConnector = nxtConnector;
            _repository = repository;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            try
            {
                await Init();
                await ScanBlockchain();
                await ListenForTransactions(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // cancellationToken was cancelled, ignore this exception
            }
        }

        private async Task Init()
        {
            await _nxtConnector.Init();
            _blockchainStatus = await _repository.GetBlockchainStatus();
        }

        /// <summary>
        /// Scans the blockchain from last known secure block id until last block
        /// </summary>
        private async Task ScanBlockchain()
        {
            // Fetch new blockchain status before we start processing transactions
            var newBlockchainStatus = await _nxtConnector.GetBlockchainStatus();

            // Fetch transactions from blockchain and process them
            var transactions = await _nxtConnector.CheckForTransactions(_blockchainStatus.LastSecureBlockTimestamp.AddSeconds(1));
            transactions.ForEach(ProcessTransaction);

            // Look for previously recorded transactions in db that has been orphaned
            var dbTransactions = await _repository.GetNonSecuredTransactions();
            foreach (var dbTransaction in dbTransactions.Where(dbTransaction => transactions.All(t => t.TransactionId != dbTransaction.TransactionId)))
            {
                await UpdatedTransaction(dbTransaction, TransactionStatus.Removed);
            }

            await UpdateBlockchainStatus(newBlockchainStatus);
        }

        /// <summary>
        /// Main function when it comes to periodically checking for new transactions.
        /// </summary>
        private async Task ListenForTransactions(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var newBlockchainStatus = await _nxtConnector.GetBlockchainStatus();
                var confirmedTransactions = await _nxtConnector.CheckForTransactions(_blockchainStatus.LastSecureBlockTimestamp.AddSeconds(1), 0);
                var dbTransactions = await _repository.GetNonSecuredTransactions();

                confirmedTransactions.ForEach(ProcessTransaction);

                // unprocessed means the transaction either became secured or orphaned (removed)
                var unprocessedDbTransactions = dbTransactions.Where(dbTransaction => 
                    confirmedTransactions.All(t => t.TransactionId != dbTransaction.TransactionId))
                    .ToList();

                foreach (var unprocessedDbTransaction in unprocessedDbTransactions)
                {
                    var transaction = await _nxtConnector.GetTransaction(unprocessedDbTransaction.TransactionId.ToUnsigned());
                    if (transaction != null)
                    {
                        var status = TransactionStatusCalculator.GetStatus(transaction);
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

                await Task.Delay(new TimeSpan(0, 0, 10), cancellationToken);
            }
        }

        private void ProcessTransaction(InboundTransaction transaction)
        {
            var dbTransaction = _repository.GetInboundTransaction(transaction.TransactionId).Result;
            if (dbTransaction == null)
            {
                NewTransaction(transaction).Wait();
            }
            else if (dbTransaction.Status != transaction.Status)
            {
                UpdatedTransaction(dbTransaction, transaction.Status).Wait();
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
