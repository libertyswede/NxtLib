using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class LastTradesReply : BaseReply
    {
        public List<AssetTrade> Trades { get; set; }
    }
}