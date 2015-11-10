using System;
using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("Amount: Nxt = {Nxt}")]
    public class Amount : IComparable<Amount>
    {
        private const long NqtMultiplier = 100000000;
        private const int MaximumNxt = 1000000000;
        private const long MaximumNqt = MaximumNxt*NqtMultiplier;

        public decimal Nxt { get; private set; }
        public long Nqt { get; private set; }

        public static readonly Amount OneNxt = CreateAmountFromNxt(1);
        public static readonly Amount OneNqt = CreateAmountFromNqt(1);

        private Amount()
        {
        }

        public static Amount CreateAmountFromNxt(decimal amountNxt)
        {
            CheckNxt(amountNxt);

            var amount = new Amount
            {
                Nxt = amountNxt,
                Nqt = (long)(amountNxt*NqtMultiplier)
            };
            return amount;
        }

        public static Amount CreateAmountFromNqt(long amountNqt)
        {
            CheckNqt(amountNqt);

            var amount = new Amount
            {
                Nxt = (decimal)amountNqt / NqtMultiplier,
                Nqt = amountNqt
            };
            return amount;
        }

        private static void CheckNqt(long amountNqt)
        {
            if (amountNqt > MaximumNqt)
            {
                throw new ArgumentException("Amount must not be larger than " + MaximumNqt, nameof(amountNqt));
            }
            if (amountNqt < 0 && IsProbablyNotGenesisAccount(amountNqt))
            {
                throw new ArgumentException("Amount must be larger than 0", nameof(amountNqt));
            }
        }

        private static bool IsProbablyNotGenesisAccount(long amountNqt)
        {
            return amountNqt > -99900000000000000;
        }

        private static void CheckNxt(decimal amountNxt)
        {
            if (amountNxt > MaximumNxt)
            {
                throw new ArgumentException("Amount must not be larger than " + MaximumNxt, nameof(amountNxt));
            }
            if (amountNxt < 0)
            {
                throw new ArgumentException("Amount must be larger than 0", nameof(amountNxt));
            }
            if (amountNxt % (1m / NqtMultiplier) > 0)
            {
                throw new ArgumentException("Amount must not have larger precision than " + 1m / NqtMultiplier, nameof(amountNxt));
            }
        }
        
        public int CompareTo(Amount other)
        {
            return Nqt < other.Nqt ? -1 : (Nqt > other.Nqt ? 1 : 0);
        }
    }
}
