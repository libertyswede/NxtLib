using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Forging
{
    public class NextBlockGenerator
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }
        public int Deadline { get; set; }
        public int HitTime { get; set; }

        [JsonConverter(typeof(NxtAmountConverter))]
        [JsonProperty(PropertyName = Parameters.EffectiveBalanceNxt)]
        public Amount EffectiveBalance { get; set; }
    }
}