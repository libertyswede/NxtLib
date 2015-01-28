using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public class GoodsReply : BaseReply
    {
        [JsonProperty(PropertyName = "goods")]
        public List<Good> GoodsList { get; set; }
    }
}