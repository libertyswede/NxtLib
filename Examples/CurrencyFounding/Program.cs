using System;
using System.Linq;
using System.Threading;
using NxtLib.MonetarySystem;

namespace CurrencyFounding
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new MonetarySystemService();
            var currency = service.GetCurrency(CurrencyLocator.ByCurrencyId(7851677590592900934)).Result;
            var founders = service.GetCurrencyFounders(7851677590592900934).Result;

            var goal = currency.ReserveSupply * currency.MinReservePerUnit.Nxt;
            var reservedTotal = founders.Founders.Sum(f => f.AmountPerUnit.Nxt)*currency.ReserveSupply;
            
            var nfi = new System.Globalization.NumberFormatInfo
            {
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = ","
            };
            
            Console.WriteLine($"Name:         {currency.Name}");
            Console.WriteLine($"Code:         {currency.Code}");
            Console.WriteLine($"Id:           {currency.CurrencyId}");
            Console.WriteLine(string.Format(nfi, "Reserve Goal: {0:0,0.0} ({1:P})", goal, reservedTotal/goal));
            Console.WriteLine($"Founders:     {founders.Founders.Count}");
            Console.WriteLine("---------------------------------------------------");

            foreach (var founder in founders.Founders.OrderByDescending(f => f.AmountPerUnit.Nqt))
            {
                var reserved = founder.AmountPerUnit.Nxt * currency.ReserveSupply;
                Console.WriteLine(string.Format(nfi, "Account: {0} - {1:0,0.0} NXT", founder.AccountRs, reserved));
            }
            Console.ReadLine();
        }
    }
}
