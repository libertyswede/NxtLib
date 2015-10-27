using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class OrderCancellation
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int Confirmations { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int Height { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Order)]
        public ulong OrderId { get; set; }
        public bool Phased { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public int TransactionHeight { get; set; }
    }
}