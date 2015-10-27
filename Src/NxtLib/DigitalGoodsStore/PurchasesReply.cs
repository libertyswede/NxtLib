using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.DigitalGoodsStore
{
    public class PurchasesReply : BaseReply
    {
        public List<Purchase> Purchases { get; set; }
    }
}