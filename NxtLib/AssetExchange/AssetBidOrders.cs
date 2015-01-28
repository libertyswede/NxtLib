using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetBidOrders : BaseReply
    {
        public List<AssetOrder> BidOrders { get; set; }
    }
}