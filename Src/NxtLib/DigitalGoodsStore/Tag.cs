using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStore
{
    public class Tag
    {
        public int InStockCount { get; set; }

        [JsonProperty(PropertyName = Parameters.Tag)]
        public string TagName { get; set; }
        public int TotalCount { get; set; }
    }
}