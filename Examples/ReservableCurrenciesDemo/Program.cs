using System;
using System.Linq;
using System.Threading.Tasks;
using NxtLib;
using NxtLib.MonetarySystem;

namespace ReservableCurrenciesDemo
{
    public class Program
    {
        public static void Main()
        {
            Task.WaitAll(PrintReservableCurrencies());
        }

        private static async Task PrintReservableCurrencies()
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
                var goal = currency.ReserveSupply*currency.MinReservePerUnit.Nxt;

                Console.WriteLine("----------------------------");
                Console.WriteLine($"Name: {currency.Name}");
                Console.WriteLine($"Founders: {founders.Founders.Count}");
                Console.WriteLine($"Reserve Goal: {goal:n} NXT");
                if (founders.Founders.Count > 0)
                {
                    var reserverdPerUnitNxt = founders.Founders.Sum(f => f.AmountPerUnit.Nxt);
                    var reservedNxt = reserverdPerUnitNxt * currency.ReserveSupply;

                    total += reservedNxt;
                    Console.WriteLine($"Amount: {reservedNxt:n} NXT ({reservedNxt / goal:P})");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine($"Total reserved: {total:n} NXT");
            Console.WriteLine("");
            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }
    }
}
