using System;
using System.Threading;
using System.Threading.Tasks;
using NxtExchange.DAL;
using NxtLib.Accounts;
using NxtLib.Blocks;
using NxtLib.Messages;
using NxtLib.ServerInfo;
using NxtLib.Transactions;

namespace NxtExchange
{
    class Program
    {
        private const string SecretPhrase = "abc123";
        private const string NxtUri = "http://localhost:6876/nxt";

        static void Main()
        {
            var nxtService = new NxtConnector(SecretPhrase, new AccountService(NxtUri), new BlockService(NxtUri),
                new MessageService(NxtUri), new ServerInfoService(NxtUri), new TransactionService(NxtUri));
            var controller = new ExchangeController(nxtService, new NxtRepository());

            controller.IncomingTransaction += OnIncomingTransaction;
            controller.UpdatedTransactionStatus += OnUpdatedTransactionStatus;

            var cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => controller.Start(cts.Token), TaskCreationOptions.LongRunning);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
            Console.WriteLine("Shutting down executing tasks... ");
            cts.Cancel();
        }

        private static void OnIncomingTransaction(object sender, IncomingTransactionEventArgs eventArgs)
        {
            var transaction = eventArgs.Transaction;

            Console.WriteLine("");
            Console.WriteLine("*** New incoming transaction ***");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Transaction ID: {0}", transaction.TransactionId);
            Console.WriteLine("         Amount: {0} NXT", transaction.GetAmount().Nxt);
            Console.WriteLine("        Message: {0}", transaction.DecryptedMessage);
            Console.WriteLine("         Status: {0}", transaction.Status);
            Console.WriteLine("----------------------------------------");
        }

        private static void OnUpdatedTransactionStatus(object sender, StatusUpdatedEventArgs eventArgs)
        {
            var transaction = eventArgs.Transaction;

            Console.WriteLine("");
            Console.WriteLine("*** Updated transaction status ***");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(" Transaction ID: {0}", transaction.TransactionId);
            Console.WriteLine("Previous status: {0}", eventArgs.PreviousStatus);
            Console.WriteLine("     New status: {0}", transaction.Status);
            Console.WriteLine("----------------------------------------");
        }
    }
}
