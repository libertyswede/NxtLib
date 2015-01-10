using System;
using System.Threading.Tasks;
using NxtLib;
using NxtLib.AccountOperations;

namespace NxtConsoleDemo
{
    public class NxtManager
    {
        private readonly IAccountService _accountService;

        public NxtManager(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task Start()
        {
            int choice;

            do
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Welcome to the Nxt Console Demo");
                Console.WriteLine("");
                Console.WriteLine("Available commands are:");
                Console.WriteLine("1) Check balance");
                Console.WriteLine("2) Send Nxt");
                Console.WriteLine("3) Exit");
                Console.WriteLine("-------------------------------");
                Console.Write("What is your command: ");
                choice = GetIntFromConsole();

                if (choice == 1)
                {
                    Console.Write("Enter your Nxt account id: ");
                    var accountId = Console.ReadLine();
                    var reply = await _accountService.GetBalance(accountId);
                    Console.WriteLine("Current balance: {0} Nxt", reply.Balance.Nxt);
                }
                else if (choice == 2)
                {
                    Console.Write("Enter your secret passphrase: ");
                    var secret = Console.ReadLine();
                    Console.Write("Enter recipient account id: ");
                    var recipientId = Console.ReadLine();
                    Console.Write("Enter number of Nxt to send: ");
                    var amount = Amount.CreateAmountFromNxt(GetIntFromConsole());
                    var createTransactionParameters = new CreateTransactionBySecretPhrase(false, 1440,
                        Amount.CreateAmountFromNxt(1), secret);
                    var reply = await _accountService.SendMoney(createTransactionParameters, recipientId, amount);
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