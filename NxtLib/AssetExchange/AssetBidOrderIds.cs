using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetBidOrderIds : BaseReply
    {
        public List<ulong> BidOrderIds { get; set; }
    }
}