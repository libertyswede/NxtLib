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
            controller.IncomingTransaction += OnIncomingTransaction;
            controller.Start().Wait();
            
            Console.WriteLine("All done, press enter to exit");
            Console.ReadLine();
        }

        private static void OnIncomingTransaction(object sender, IncomingTransactionEventArgs eventArgs)
        {
            var transaction = eventArgs.Transaction;
            var amount = transaction.GetAmount();

            Console.WriteLine("*** New incoming transaction ***");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Transaction ID: {0}", transaction.TransactionId);
            Console.WriteLine("        Amount: {0} NXT", amount.Nxt);
            Console.WriteLine("      Customer: {0}", transaction.CustomerId);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
        }
    }
}
