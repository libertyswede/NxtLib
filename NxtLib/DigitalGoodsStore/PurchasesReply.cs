using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.DigitalGoodsStore
{
    public class PurchasesReply : BaseReply
    {
        [JsonProperty(PropertyName = "purchases")]
        public List<Purchase> GoodsPurchases { get; set; }
    }
}