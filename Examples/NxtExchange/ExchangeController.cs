using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NxtExchange.DAL;

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
            foreach (var transaction in transactions)
            {
                await VerifyTransaction(transaction);
            }
        }

        private async Task VerifyTransaction(InboundTransaction transaction)
        {
            var dbTransaction = await _repository.GetInboundTransactionAsync(transaction.TransactionId);
            if (dbTransaction == null)
            {
                ProcessTransaction(transaction);
            }

        }

        private void ProcessTransaction(InboundTransaction transaction)
        {
            var accountRegex = new Regex("account:\\s?([\\d]+)", RegexOptions.IgnoreCase);
            var match = accountRegex.Match(transaction.DecryptedMessage);
            if (match.Success)
            {
                transaction.CustomerId = Convert.ToInt32(match.Groups[1].Value);
            }
            _repository.AddInboundTransactionAsync(transaction);
        }

        private async Task Init()
        {
            await _nxtService.Init();
            _blockchainStatus = await _repository.GetBlockchainStatusAsync();
        }
    }
}
