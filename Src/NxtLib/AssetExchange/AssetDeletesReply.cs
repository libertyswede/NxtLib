using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetDeletesReply : BaseReply
    {
        public IEnumerable<AssetDelete> Deletes { get; set; }
    }
}