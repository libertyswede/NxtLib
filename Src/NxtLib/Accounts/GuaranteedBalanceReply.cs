using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class GuaranteedBalanceReply : BaseReply
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.GuaranteedBalanceNqt)]
        public Amount GuaranteedBalance { get; set; }
    }
}