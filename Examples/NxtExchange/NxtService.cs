using System.Threading.Tasks;
using NxtLib.Accounts;
using NxtLib.Blocks;

namespace NxtExchange
{
    public interface INxtService
    {
        Task Init();
    }

    public class NxtService : INxtService
    {
        private readonly IAccountService _accountService;
        private readonly IBlockService _blockService;
        private readonly string _secretPhrase;
        private string _accountRs;
        private string _publicKey;

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
    }
}
