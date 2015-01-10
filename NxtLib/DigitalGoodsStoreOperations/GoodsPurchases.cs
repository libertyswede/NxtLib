using System.Collections.Generic;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public class GoodsPurchases : BaseReply
    {
        public List<GoodPurchase> Purchases { get; set; }
    }
}