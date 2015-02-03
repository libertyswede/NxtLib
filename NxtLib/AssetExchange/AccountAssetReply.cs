namespace NxtLib.AssetExchange
{
    public class AccountAssetReply : AccountAsset, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}