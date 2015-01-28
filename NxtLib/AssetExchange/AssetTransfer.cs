using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetOperations
{
    public class AssetTransfer
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "asset")]
        public ulong AssetId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "assetTransfer")]
        public ulong AssetTransferId { get; set; }
        public int Decimals { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "recipient")]
        public ulong RecipientId { get; set; }
        public string RecipientRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Sender { get; set; }
        public string SenderRs { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}