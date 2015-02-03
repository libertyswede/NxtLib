using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class BalanceReply : BaseReply
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "unconfirmedBalanceNqt")]
        public Amount UnconfirmedBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "guaranteedBalanceNqt")]
        public Amount GuaranteedBalance { get; set; }

        [JsonConverter(typeof(NxtAmountConverter))]
        [JsonProperty(PropertyName = "effectiveBalanceNxt")]
        public Amount EffectiveBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "forgedBalanceNqt")]
        public Amount ForgedBalance { get; set; }

        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "balanceNqt")]
        public Amount Balance { get; set; }
    }
}