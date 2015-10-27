using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public abstract class AssetTransferBase
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Asset)]
        public ulong AssetId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.AssetTransfer)]
        public ulong AssetTransferId { get; set; }
        public int Decimals { get; set; }
        public int Height { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Recipient)]
        public ulong RecipientId { get; set; }
        public string RecipientRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Sender)]
        public ulong SenderId { get; set; }
        public string SenderRs { get; set; }
    }
}