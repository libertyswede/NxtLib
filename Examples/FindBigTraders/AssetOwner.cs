namespace FindBigTraders
{
    public class AssetOwner
    {
        public ulong AccountId { get; set; }
        public long QuantityQnt { get; set; }

        public AssetOwner(ulong accountId, long quantityQnt)
        {
            AccountId = accountId;
            QuantityQnt = quantityQnt;
        }
    }
}
