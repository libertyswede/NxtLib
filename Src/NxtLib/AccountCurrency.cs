using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class AccountCurrency : CurrencyInfo
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long UnconfirmedUnits { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Units { get; set; }
    }
}