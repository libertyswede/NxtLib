using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Phasing
{
    public class PhasingPollVote
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = "transaction")]
        public ulong TransactionId { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Voter { get; set; }
        public string VoterRs { get; set; }
    }
}