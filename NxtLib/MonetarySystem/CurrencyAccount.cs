using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class CurrencyAccount
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long UnconfirmedUnits { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Units { get; set; }
    }
}