using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class AssetDelete
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonProperty(PropertyName = Parameters.Asset)]
        public ulong AssetId { get; set; }

        [JsonProperty(PropertyName = Parameters.AssetDelete)]
        public ulong AssetDeleteId { get; set; }
        public int Height { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }
    }
}