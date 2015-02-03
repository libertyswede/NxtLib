namespace NxtLib.AssetExchange
{
    public class AssetReply : Asset, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}