using System;

namespace NxtLib
{
    public class Amount
    {
        private const long NqtMultiplier = 100000000;
        private const int MaximumNxt = 1000000000;
        private const long MaximumNqt = MaximumNxt*NqtMultiplier;

        public decimal Nxt { get; private set; }
        public long Nqt { get; private set; }

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
                throw new ArgumentException("Amount must not be larger than " + MaximumNqt, "amountNqt");
            }
            if (amountNqt < 0 && IsProbablyNotGenesisAccount(amountNqt))
            {
                throw new ArgumentException("Amount must be larger than 0", "amountNqt");
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
                throw new ArgumentException("Amount must not be larger than " + MaximumNxt, "amountNxt");
            }
            if (amountNxt < 0)
            {
                throw new ArgumentException("Amount must be larger than 0", "amountNxt");
            }
            if (amountNxt % (1m / NqtMultiplier) > 0)
            {
                throw new ArgumentException("Amount must not have larger precision than " + (1m / NqtMultiplier), "amountNxt");
            }
        }
    }
}
