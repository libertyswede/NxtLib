using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetOperations
{
    public class Asset : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "asset")]
        public ulong AssetId { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int NumberOfAccounts { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfTransfers { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }
    }
}