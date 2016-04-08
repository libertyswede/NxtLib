using System.Collections.Generic;

namespace NxtLib.DigitalGoodsStore
{
    public class PurchaseReply : Purchase, IBaseReply
    {
        public string DecryptedGoods { get; set; }
        public IEnumerable<KeyValuePair<string, string>> PostData { get; set; }
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}