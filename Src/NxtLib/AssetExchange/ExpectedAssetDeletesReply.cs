using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class ExpectedAssetDeletesReply : BaseReply
    {
        public IEnumerable<ExpectedAssetDelete> Deletes { get; set; }
    }
}