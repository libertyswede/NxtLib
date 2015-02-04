using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class Currency
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }
        public byte Algorithm { get; set; }
        public string Code { get; set; }
        public int CreationHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long CurrentReservePerUnitNqt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long CurrentSupply { get; set; }
        public byte Decimals { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long InitialSupply { get; set; }
        public int IssuanceHeight { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long MaxSupply { get; set; }
        public int MaxDifficulty { get; set; }
        public int MinDifficulty { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "minReservePerUnitNQT")]
        public Amount MinReservePerUnit { get; set; }
        public string Name { get; set; }
        public int NumberOfExchanges { get; set; }
        public int NumberOfTransfers { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long ReserveSupply { get; set; }
        public int Type { get; set; }

        [JsonConverter(typeof(CurrencyTypeConverter))]
        public HashSet<CurrencyType> Types { get; set; }
    }
}