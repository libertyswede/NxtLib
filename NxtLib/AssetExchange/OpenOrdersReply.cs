using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class OpenOrdersReply : BaseReply
    {
        public List<AssetOrder> OpenOrders { get; set; }
    }
}