using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NxtExchange.DAL;
using NxtLib;

namespace NxtExchange
{
    public class ExchangeController
    {
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
            transactions.ForEach(VerifyTransaction);
        }

        private void VerifyTransaction(InboundTransaction transaction)
        {
            var dbTransaction = _repository.GetInboundTransactionAsync(transaction.TransactionId);
            if (dbTransaction == null)
            {
                NewTransaction(transaction);
            }

        }

        private void NewTransaction(InboundTransaction transaction)
        {

            _repository.AddInboundTransactionAsync(transaction);
        }

        private async Task Init()
        {
            await _nxtService.Init();
            _blockchainStatus = await _repository.GetBlockchainStatusAsync();
        }
    }
}
