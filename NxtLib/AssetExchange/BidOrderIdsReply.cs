using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class BidOrderIdsReply : BaseReply
    {
        public List<ulong> BidOrderIds { get; set; }
    }
}