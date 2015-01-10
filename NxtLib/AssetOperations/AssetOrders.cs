using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetOrders : BaseReply
    {
        public List<AssetExchangeOrder> OpenOrders { get; set; }
    }
}