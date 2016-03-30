using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class MonitoredAccount
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public string AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Amount { get; set; }
        public int Interval { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Threshold { get; set; }
    }
}