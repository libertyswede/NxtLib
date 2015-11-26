using System;
using System.Collections.Generic;
using NxtLib.Local;
using NxtLib;

namespace Dividends
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = Parse(args);
            if (options.Mode == Mode.Transaction)
            {
                var dividendPayoutListing = new DividendPayoutListing();
                dividendPayoutListing.List(options.Id);
            }
            else
            {
                var divListing = new DividendListing();
                divListing.List(options);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static ProgramOptions Parse(IReadOnlyList<string> args)
        {
            if (args.Count == 0)
            {
                return new ProgramOptions { Mode = Mode.All };
            }
            if (args.Count == 2)
            {
                ulong id;
                if (args[0].Equals("-account"))
                {
                    if (ulong.TryParse(args[1], out id))
                    {
                        return new ProgramOptions { Id = id, Mode = Mode.Account };
                    }
                    if (args[1].StartsWith("NXT-"))
                    {
                        return new ProgramOptions
                        {
                            Id = new Account(args[1]).AccountId,
                            Mode = Mode.Account
                        };
                    }
                }
                if (args[0].Equals("-transaction") && ulong.TryParse(args[1], out id))
                {
                    return new ProgramOptions { Id = id, Mode = Mode.Transaction };
                }
                if (args[0].Equals("-asset") && ulong.TryParse(args[1], out id))
                {
                    return new ProgramOptions { Id = id, Mode = Mode.Asset };
                }
            }
            Console.WriteLine("Usage: ");
            Console.WriteLine("No arguments = fetch all dividends");
            Console.WriteLine("-account [accountId] = fetch dividends by assets owned by [accountId]");
            Console.WriteLine("-transaction [transactionId] = fetch details about a specific [transactionId]");
            Console.WriteLine("-asset [assetId] = fetch dividends by assets with specidied [assetId]");
            throw new NotSupportedException();
        }
    }
}
