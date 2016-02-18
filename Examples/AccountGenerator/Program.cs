using System;
using NxtLib.Accounts;
using NxtLib.Local;

namespace AccountGenerator
{
    internal class Program
    {
        private static void Main()
        {
            var generator = new LocalPasswordGenerator();
            var accountService = new LocalAccountService();

            Console.Write("Enter desired ending:");
            var ending = Console.ReadLine();

            var secretPhrase = generator.GeneratePasswordWithAccountEnding(ending);
            var account = accountService.GetAccount(AccountIdLocator.BySecretPhrase(secretPhrase));

            Console.WriteLine($"Generated NXT Address: {account.AccountRs}");
            Console.WriteLine($"Secret phrase: {secretPhrase}");
            Console.WriteLine("Press enter to quit.");
            Console.ReadLine();
        }
    }
}
