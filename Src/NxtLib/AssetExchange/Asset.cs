using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.AssetExchange
{
    public class Asset
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Asset)]
        public ulong AssetId { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long InitialQuantityQnt { get; set; }
        public string Name { get; set; }
        public int NumberOfAccounts { get; set; }
        public int NumberOfTrades { get; set; }
        public int NumberOfTransfers { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long QuantityQnt { get; set; }
    }
}