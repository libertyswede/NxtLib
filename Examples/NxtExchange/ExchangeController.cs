using System.Data.Entity;
using System.Threading.Tasks;
using NxtExchange.DAL;

namespace NxtExchange
{
    public class ExchangeController
    {
        private readonly INxtService _nxtService;
        private readonly INxtContextFactory _contextFactory;
        private BlockchainStatus _blockchainStatus;

        public ExchangeController(INxtService nxtService, INxtContextFactory contextFactory)
        {
            _nxtService = nxtService;
            _contextFactory = contextFactory;
        }

        public async Task Start()
        {
            await Init();
            await _nxtService.ScanBlockchain(_blockchainStatus.LastConfirmedBlockId.ToUnsigned());
        }

        private async Task Init()
        {
            await _nxtService.Init();
            using (var context = _contextFactory.Create())
            {
                _blockchainStatus = await context.BlockchainStatus.SingleAsync();
            }
        }
    }
}
