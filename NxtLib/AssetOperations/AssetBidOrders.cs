using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetBidOrders : BaseReply
    {
        public List<AssetExchangeOrder> BidOrders { get; set; }
    }
}