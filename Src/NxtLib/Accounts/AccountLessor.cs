using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountLessor
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.GuaranteedBalanceNqt)]
        public Amount GuaranteedBalance { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Lessor)]
        public ulong LessorAccount { get; set; }
        public string LessorRs { get; set; }
    }
}