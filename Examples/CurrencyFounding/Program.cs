using System;
using System.Linq;
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

            Console.WriteLine($"Name:         {currency.Name}");
            Console.WriteLine($"Code:         {currency.Code}");
            Console.WriteLine($"Id:           {currency.CurrencyId}");
            Console.WriteLine($"Reserve Goal: {goal:0,0.0}");
            Console.WriteLine("---------------------------------------------------");

            foreach (var founder in founders.Founders.OrderByDescending(f => f.AmountPerUnit.Nqt))
            {
                var reserved = founder.AmountPerUnit.Nxt * currency.ReserveSupply;
                Console.WriteLine($"Account:  {founder.AccountRs} - {reserved:0,0.0} NXT");
            }
            Console.ReadLine();
        }
    }
}
