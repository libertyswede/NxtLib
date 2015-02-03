using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AskOrderIdsReply : BaseReply
    {
        public List<ulong> AskOrderIds { get; set; }
    }
}