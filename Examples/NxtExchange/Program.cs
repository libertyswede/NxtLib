using NxtExchange.DAL;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "abc123";

        static void Main()
        {
            var nxtService = new NxtService(SecretPhrase, new AccountService(), new BlockService(), new MessageService());
            var controller = new ExchangeController(nxtService, new NxtRepository());
            controller.Start().Wait();
        }
    }
}
