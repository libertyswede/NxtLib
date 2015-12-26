using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class AvailableForExchangeReply : BaseReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long AmountNqt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long RateNqt { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long Units { get; set; }
    }
}