using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
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
        public ulong QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong UnconfirmedQuantityQnt { get; set; }
    }
}