using NxtExchange.DAL;
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
            var nxtService = new NxtService(SecretPhrase, new AccountService(), new BlockService());
            var context = new NxtContext();
            var controller = new ExchangeController(nxtService, context);
            controller.Start().Wait();
        }
    }
}
