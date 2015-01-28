using System.Collections.Generic;

namespace NxtLib.AssetExchange
{
    public class AssetOrders : BaseReply
    {
        public List<AssetOrder> OpenOrders { get; set; }
    }
}