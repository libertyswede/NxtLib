using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class GuaranteedBalanceReply : BaseReply
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        [JsonProperty(PropertyName = "guaranteedBalanceNQT")]
        public Amount GuaranteedBalance { get; set; }
    }
}