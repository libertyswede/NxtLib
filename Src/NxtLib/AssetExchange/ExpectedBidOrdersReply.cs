using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class ExpectedBidOrdersReply : BaseReply
    {
        public List<ExpectedAssetOrder> BidOrders { get; set; }
    }
}