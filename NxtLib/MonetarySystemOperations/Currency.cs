using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystemOperations
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

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long MinReservePerUnitNqt { get; set; }
        public string Name { get; set; }
        public int NumberOfExchanges { get; set; }
        public int NumberOfTransfers { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long ReserveSupply { get; set; }
        public int Type { get; set; }

        [JsonConverter(typeof(CurrencyTypeConverter))]
        public List<CurrencyType> Types { get; set; }
    }

    public enum CurrencyType
    {
        [Description("EXCHANGEABLE")]
        Exchangeable = 0x01,
        [Description("CONTROLLABLE")]
        Controllable = 0x02,
        [Description("RESERVABLE")]
        Reservable = 0x04,
        [Description("CLAIMABLE")]
        Claimable = 0x08,
        [Description("MINTABLE")]
        Mintable = 0x10,
        [Description("NON_SHUFFLEABLE")]
        NonShuffleable = 0x20
    }
}