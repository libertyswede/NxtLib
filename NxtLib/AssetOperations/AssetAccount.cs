using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetOperations
{
    public class AssetAccount
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "asset")]
        public ulong AssetId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long UnconfirmedQuantityQnt { get; set; }
    }
}