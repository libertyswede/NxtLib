using System;
using System.Linq;
using System.Threading.Tasks;
using NxtLib;
using NxtLib.MonetarySystem;

namespace ReservableCurrenciesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(PrintReservableCurrencies());
        }

        private async static Task PrintReservableCurrencies()
        {
            var service = new MonetarySystemService();
            var allCurrencies = await service.GetAllCurrencies();
            var reservableCurrencies = allCurrencies.Currencies
                .Where(c => c.Types.Contains(CurrencyType.Reservable))
                .OrderBy(c => c.Name)
                .ToList();

            Console.WriteLine("** Listing of all reservable currencies **");
            Console.WriteLine("");
            var total = 0M;

            foreach (var currency in reservableCurrencies)
            {
                var founders = await service.GetCurrencyFounders(currency.CurrencyId);

                Console.WriteLine("----------------------------");
                Console.WriteLine("Name: {0}", currency.Name);
                Console.WriteLine("Founders: {0}", founders.Founders.Count);
                if (founders.Founders.Count > 0)
                {
                    var reserved = founders.Founders.Sum(f => f.AmountPerUnit.Nxt);
                    total += reserved;
                    Console.WriteLine("Amount: {0} NXT", reserved);
                }
                Console.WriteLine("");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Total reserved: {0} NXT", total);
            Console.WriteLine("");
            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }
    }
}
