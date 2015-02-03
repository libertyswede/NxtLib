namespace NxtLib.DigitalGoodsStore
{
    public class PurchaseReply : Purchase, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}