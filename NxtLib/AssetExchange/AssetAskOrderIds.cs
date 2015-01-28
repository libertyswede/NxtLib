using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetAskOrderIds : BaseReply
    {
        public List<ulong> AskOrderIds { get; set; }
    }
}