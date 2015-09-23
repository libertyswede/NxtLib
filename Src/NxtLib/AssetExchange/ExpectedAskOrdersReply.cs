using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class ExpectedAskOrdersReply : BaseReply
    {
        public List<ExpectedAssetOrder> AskOrders { get; set; }
    }
}