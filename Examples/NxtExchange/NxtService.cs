using System.Threading.Tasks;
using NxtLib.Accounts;
using NxtLib.Blocks;

namespace NxtExchange
{
    public interface INxtService
    {
    }

    public class NxtService : INxtService
    {
        private readonly ExchangeSettings _exchangeSettings;
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;

        public NxtService(ExchangeSettings exchangeSettings, IAccountService accountService, IBlockService blockService)
        {
            _exchangeSettings = exchangeSettings;
            _accountService = accountService;
            _blockService = blockService;
        }

        public async Task StartListeningToNxt()
        {
            var block = await _blockService.GetBlock(BlockLocator.BlockId(_exchangeSettings.LastSecureBlockId));
            _accountService.GetAccountTransactions(_exchangeSettings.NxtAddress)
        }
    }
}
