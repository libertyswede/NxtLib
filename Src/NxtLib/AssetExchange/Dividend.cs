using Newtonsoft.Json;
using NxtLib.Internal;
using System;

namespace NxtLib.AssetExchange
{
    public class Dividend
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong AmountNQTPerQnt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Asset)]
        public ulong AssetId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong AssetDividend { get; set; }
        public int DividendHeight { get; set; }
        public int Height { get; set; }
        public int NumberOfAccounts { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long TotalDividend { get; set; }
    }
}
