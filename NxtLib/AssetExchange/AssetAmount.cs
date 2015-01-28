using System;

namespace NxtLib.AssetExchange
{
    public class AssetAmount
    {
        public int Decimals { get; private set; }
        public decimal Nxt { get; private set; }
        public ulong Nqt { get; private set; }

        private AssetAmount()
        {
        }

        public static AssetAmount CreateAmountFromNxt(decimal amountNxt, int decimals)
        {
            var amount = new AssetAmount
            {
                Nxt = amountNxt,
                Nqt = (ulong)(amountNxt * (decimal)Math.Pow(10, decimals)),
                Decimals = decimals
            };
            return amount;
        }

        public static AssetAmount CreateAmountFromNqt(ulong amountNqt, int decimals)
        {
            var amount = new AssetAmount
            {
                Nxt = amountNqt / (decimal)Math.Pow(10, decimals),
                Nqt = amountNqt,
                Decimals = decimals
            };
            return amount;
        }
    }
}
