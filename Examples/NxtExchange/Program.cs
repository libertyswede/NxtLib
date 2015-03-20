using System;
using NxtExchange.DAL;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;
using NxtLib.ServerInfo;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "abc123";
        private const string NxtUri = "http://localhost:6876/nxt";

        static void Main()
        {
            var nxtService = new NxtService(SecretPhrase, new AccountService(NxtUri), new BlockService(NxtUri),
                new MessageService(NxtUri), new ServerInfoService(NxtUri));
            var controller = new ExchangeController(nxtService, new NxtRepository());
            controller.IncomingTransaction += OnIncomingTransaction;
            controller.UpdatedTransactionStatus += OnUpdatedTransactionStatus;
            controller.Start().Wait();
            
            Console.WriteLine("All done, press enter to exit");
            Console.ReadLine();
        }

        private static void OnIncomingTransaction(object sender, IncomingTransactionEventArgs eventArgs)
        {
            var transaction = eventArgs.Transaction;

            Console.WriteLine("*** New incoming transaction ***");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Transaction ID: {0}", transaction.TransactionId);
            Console.WriteLine("         Amount: {0} NXT", transaction.GetAmount().Nxt);
            Console.WriteLine("       Customer: {0}", transaction.CustomerId);
            Console.WriteLine("         Status: {0}", transaction.Status);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
        }

        private static void OnUpdatedTransactionStatus(object sender, StatusUpdatedEventArgs eventArgs)
        {
            var transaction = eventArgs.Transaction;

            Console.WriteLine("*** Updated transaction status ***");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Transaction ID: {0}", transaction.TransactionId);
            Console.WriteLine("Previous status: {0}", eventArgs.PreviousStatus);
            Console.WriteLine("     New status: {0}", transaction.Status);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");
        }
    }
}
