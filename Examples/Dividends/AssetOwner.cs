namespace Dividends
{
    public class AssetOwner
    {
        public string AccountRs { get; set; }
        public long QuantityQnt { get; set; }

        public AssetOwner(string accountRs, long quantityQnt)
        {
            AccountRs = accountRs;
            QuantityQnt = quantityQnt;
        }

        public AssetOwner(string accountRs, ulong quantityQnt)
            : this(accountRs, (long)quantityQnt)
        {
        }
    }
}