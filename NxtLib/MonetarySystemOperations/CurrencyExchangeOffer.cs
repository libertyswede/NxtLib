using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystemOperations
{
    public class CurrencyExchangeOffer
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "account")]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "currency")]
        public ulong CurrencyId { get; set; }
        public int ExpirationHeight { get; set; }
        public int Height { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Limit { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "offer")]
        public ulong OfferId { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "rateNQT")]
        public Amount Rate { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Supply { get; set; }
    }
}