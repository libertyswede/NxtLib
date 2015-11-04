using System.Collections.Generic;

namespace NxtLib.DigitalGoodsStore
{
    public class PurchasesReply : BaseReply
    {
        public List<Purchase> Purchases { get; set; }
    }
}