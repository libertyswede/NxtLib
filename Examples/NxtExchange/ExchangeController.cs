using System.Data.Entity;
using System.Threading.Tasks;
using NxtExchange.DAL;

namespace NxtExchange
{
    public class ExchangeController
    {
        private readonly INxtService _nxtService;
        private readonly INxtContext _nxtContext;
        private const ulong GenesisBlockId = 2680262203532249785;

        public ExchangeController(INxtService nxtService, INxtContext nxtContext)
        {
            _nxtService = nxtService;
            _nxtContext = nxtContext;
        }

        public async Task Start()
        {
            await Init();
        }

        private async Task Init()
        {
            await _nxtService.Init();
            var blockchainStatus = await _nxtContext.BlockchainStatus.FirstOrDefaultAsync();
            if (blockchainStatus == null)
            {
                blockchainStatus = new BlockchainStatus
                {
                    LastConfirmedBlockId = (long) GenesisBlockId,
                    LastKnownBlockId = (long) GenesisBlockId
                };
                _nxtContext.BlockchainStatus.Add(blockchainStatus);
                await _nxtContext.SaveChangesAsync();
            }
        }
    }
}
