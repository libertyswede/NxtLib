using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class CurrencyFounder
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "amountPerUnitNQT")]
        public Amount AmountPerUnit { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }
    }
}