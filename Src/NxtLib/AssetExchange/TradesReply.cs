using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class TradesReply : BaseReply
    {
        public List<AssetTradeInfo> Trades { get; set; }
    }
}