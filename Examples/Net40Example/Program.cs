using NxtLib.Accounts;
using System;

namespace NxtLibTest40
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountService = new AccountService();
            var account = accountService.GetAccount("NXT-8MVA-XCVR-3JC9-2C7C3").Result;
            Console.WriteLine("Balance is: " + account.Balance.Nxt + " NXT");
            Console.ReadLine();
        }
    }
}
