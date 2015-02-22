using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class BidOrdersReply : BaseReply
    {
        public List<AssetOrder> BidOrders { get; set; }
    }
}