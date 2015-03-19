using System;
using NxtExchange.DAL;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "abc123";
        private const string NxtUri = "http://localhost:6876/nxt";

        static void Main()
        {
            var nxtService = new NxtService(SecretPhrase, new AccountService(NxtUri), new BlockService(NxtUri), new MessageService(NxtUri));
            var controller = new ExchangeController(nxtService, new NxtRepository());
            controller.Start().Wait();
            Console.ReadLine();
        }
    }
}
