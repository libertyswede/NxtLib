using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetAskOrderIds : BaseReply
    {
        public List<ulong> AskOrderIds { get; set; }
    }
}