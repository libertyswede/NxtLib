using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetBidOrderIds : BaseReply
    {
        public List<ulong> BidOrderIds { get; set; }
    }
}