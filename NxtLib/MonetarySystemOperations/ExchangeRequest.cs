using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystemOperations
{
    public class ExchangeRequest : CurrencyInfo
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Rate { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public byte SubType { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Units { get; set; }
    }
}