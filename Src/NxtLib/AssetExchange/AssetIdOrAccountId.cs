namespace NxtLib.AssetExchange
{
    public class AssetIdOrAccountId
    {
        public string AccountId { get; private set; }
        public ulong? AssetId { get; private set; }

        public AssetIdOrAccountId(string accountId, ulong assetId)
        {
            AccountId = accountId;
            AssetId = assetId;
        }

        public AssetIdOrAccountId(string accountId)
        {
            AccountId = accountId;
        }

        public AssetIdOrAccountId(ulong assetId)
        {
            AssetId = assetId;
        }
    }
}