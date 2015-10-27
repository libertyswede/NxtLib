using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Forging
{
    public class GetForgingReply : StartForgingReply
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }
        public int Remaining { get; set; }
    }
}