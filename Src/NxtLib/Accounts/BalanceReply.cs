using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class BalanceReply : BaseReply
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.UnconfirmedBalanceNqt)]
        public Amount UnconfirmedBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.GuaranteedBalanceNqt)]
        public Amount GuaranteedBalance { get; set; }

        [JsonConverter(typeof(NxtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.EffectiveBalanceNxt)]
        public Amount EffectiveBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.ForgedBalanceNqt)]
        public Amount ForgedBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.BalanceNqt)]
        public Amount Balance { get; set; }
    }
}