using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AskOrdersReply : BaseReply
    {
        public List<AssetOrder> AskOrders { get; set; }
    }
}