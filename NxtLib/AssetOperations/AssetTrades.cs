using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetTrades : BaseReply
    {
        public List<AssetTrade> Trades { get; set; }
    }
}