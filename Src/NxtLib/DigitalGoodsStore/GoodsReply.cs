using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStore
{
    public class GoodsReply : BaseReply
    {
        [JsonProperty(PropertyName = Parameters.Goods)]
        public List<Good> GoodsList { get; set; }
    }
}