using System.Collections.Generic;

namespace NxtLib.AssetOperations
{
    public class AssetAskOrders : BaseReply
    {
        public List<AssetOrder> AskOrders { get; set; }
    }
}