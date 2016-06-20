using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetDividendsReply : BaseReply
    {
        public List<Dividend> Dividends { get; set; }
    }
}
