using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetTrades : BaseReply
    {
        public List<AssetTrade> Trades { get; set; }
    }
}