using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Shuffling
{
    public class ShufflingData
    {
        [JsonConverter(typeof(NqtAmountConverter))]
        public Amount Amount { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Assignee)]
        public long AssigneeId { get; set; }
        public string AssigneeRs { get; set; }
        public int BlocksRemaining { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Holding)]
        public long HoldingId { get; set; }
        
        // TODO: Convert to enum when stages are known
        public int HoldingType { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Issuer)]
        public long IssuerId { get; set; }
        public string IssuerRs { get; set; }
        public int ParticipantCount { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong Shuffling { get; set; }
        public BinaryHexString ShufflingFullHash { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }
        
        // TODO: Convert to enum when stages are known
        public int Stage { get; set; }

    }
}