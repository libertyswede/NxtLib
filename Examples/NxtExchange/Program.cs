using System.Threading.Tasks;
using NxtLib.Accounts;
using NxtLib.Blocks;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "Nxt Exchange Demo Program";
        private const ulong GenesisBlockId = 2680262203532249785;

        static void Main()
        {
            var accountService = new AccountService();
            var blockService = new BlockService();
            var settings = GetExchangeSettings(accountService).Result;
            var nxtService = new NxtService(settings, accountService, blockService);
            nxtService.StartListeningToNxt().Wait();
        }

        private static async Task<ExchangeSettings> GetExchangeSettings(IAccountService accountService)
        {
            var accountIdReply = await accountService.GetAccountId(AccountIdLocator.BySecretPhrase(SecretPhrase));
            return new ExchangeSettings(SecretPhrase, accountIdReply.AccountRs, accountIdReply.PublicKey, GenesisBlockId);
        }
    }
}
