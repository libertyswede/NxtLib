using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class FundingMonitor
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public string AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Amount { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Holding)]
        public ulong HoldingId { get; set; }
        public HoldingType HoldingType { get; set; }
        public int Interval { get; set; }
        public IList<MonitoredAccount> MonitoredAccounts { get; set; }
        public string Property { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Threshold { get; set; }
    }
}