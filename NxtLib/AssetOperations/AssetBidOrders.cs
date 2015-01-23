using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetBidOrders : BaseReply
    {
        public List<AssetOrder> BidOrders { get; set; }
    }
}