using System;
using NxtLib;
using NxtLib.Accounts;

namespace NxtConsoleDemo
{
    // A video describing this code in detail can be found here: https://www.youtube.com/watch?v=jc8BqEKIRjg

    class Program
    {
        static void Main()
        {
            Start();
        }

        private static void Start()
        {
            int choice;
            var service = new AccountService();

            do
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Welcome to the Nxt Console Demo");
                Console.WriteLine("");
                Console.WriteLine("Available commands are:");
                Console.WriteLine("1) Check balance");
                Console.WriteLine("2) Send nxt");
                Console.WriteLine("3) Exit");
                Console.WriteLine("----------------------");
                Console.Write("What is your command master: ");

                choice = GetIntFromConsole();

                if (choice == 1)
                {
                    Console.Write("Enter your nxt account id: ");
                    var accountId = Console.ReadLine();
                    var reply = service.GetBalance(accountId).Result;
                    Console.WriteLine("Current balance: {0} Nxt", reply.Balance.Nxt);
                }
                else if (choice == 2)
                {
                    Console.Write("Enter your secret passphrase: ");
                    var pass = Console.ReadLine();
                    Console.Write("Enter recipient account id: ");
                    var recipient = Console.ReadLine();
                    Console.Write("Enter number of Nxt to send: ");
                    var amount = Amount.CreateAmountFromNxt(GetIntFromConsole());

                    var transactionParameters = new CreateTransactionBySecretPhrase(true, 60,
                        Amount.CreateAmountFromNxt(1), pass);
                    var reply = service.SendMoney(transactionParameters, recipient, amount).Result;
                    var transaction = reply.Transaction;
                    Console.WriteLine("{0} Nxt was sent to {1} in transaction {2}", transaction.Amount.Nxt,
                        transaction.RecipientRs, transaction.TransactionId);
                }

            } while (choice != 3);
        }

        private static int GetIntFromConsole()
        {
            var value = Console.ReadLine();
            return Convert.ToInt32(value);
        }
    }
}
