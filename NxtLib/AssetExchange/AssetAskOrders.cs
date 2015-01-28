using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetAskOrders : BaseReply
    {
        public List<AssetOrder> AskOrders { get; set; }
    }
}