namespace NxtLib.AssetExchange
{
    public class AssetIdOrAccountId
    {
        public string AccountId { get; private set; }
        public ulong? AssetId { get; private set; }

        private AssetIdOrAccountId(string accountId, ulong assetId)
        {
            AccountId = accountId;
            AssetId = assetId;
        }

        private AssetIdOrAccountId(string accountId)
        {
            AccountId = accountId;
        }

        private AssetIdOrAccountId(ulong assetId)
        {
            AssetId = assetId;
        }

        public static AssetIdOrAccountId ByAssetId(ulong assetId)
        {
            return new AssetIdOrAccountId(assetId);
        }

        public static AssetIdOrAccountId ByAccountId(string accountId)
        {
            return new AssetIdOrAccountId(accountId);
        }

        public static AssetIdOrAccountId ByAssetIdAndAccountId(ulong assetId, string accountId)
        {
            return new AssetIdOrAccountId(accountId, assetId);
        }
    }
}