using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Shuffling
{
    public class ShufflingParticipant
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.NextAccount)]
        public ulong NextAccountId { get; set; }
        public string NextAccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Shuffling)]
        public ulong ShufflingId { get; set; }
        public ShufflingParticipantState State { get; set; }
    }
}