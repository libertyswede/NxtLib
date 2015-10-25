namespace NxtLib.AssetExchange
{
    public class AssetIdOrAccountId
    {
        public ulong? AccountId { get; private set; }
        public ulong? AssetId { get; private set; }

        private AssetIdOrAccountId(Account account, ulong assetId)
        {
            AccountId = account.AccountId;
            AssetId = assetId;
        }

        private AssetIdOrAccountId(Account account)
        {
            AccountId = account.AccountId;
        }

        private AssetIdOrAccountId(ulong assetId)
        {
            AssetId = assetId;
        }

        public static AssetIdOrAccountId ByAssetId(ulong assetId)
        {
            return new AssetIdOrAccountId(assetId);
        }

        public static AssetIdOrAccountId ByAccountId(Account account)
        {
            return new AssetIdOrAccountId(account);
        }

        public static AssetIdOrAccountId ByAssetIdAndAccountId(ulong assetId, Account account)
        {
            return new AssetIdOrAccountId(account, assetId);
        }
    }
}