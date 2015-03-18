using NxtExchange.DAL;
using NxtLib.Accounts;
using NxtLib.Blocks;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "abc123";

        static void Main()
        {
            var nxtService = new NxtService(SecretPhrase, new AccountService(), new BlockService());
            var controller = new ExchangeController(nxtService, new NxtContextFactory());
            controller.Start().Wait();
        }
    }
}
