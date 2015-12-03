using System.Collections;
using System.Collections.Generic;
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
        public ulong? AssigneeId { get; set; }
        public string AssigneeRs { get; set; }
        public int BlocksRemaining { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Holding)]
        public ulong HoldingId { get; set; }
        public HoldingInfo HoldingInfo { get; set; }
        public HoldingType HoldingType { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Issuer)]
        public ulong IssuerId { get; set; }
        public string IssuerRs { get; set; }
        public int ParticipantCount { get; set; }
        public IList<BinaryHexString> RecipientPublicKeys { get; set; }
        public int RegistrantCount { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Shuffling)]
        public ulong ShufflingId { get; set; }
        public BinaryHexString ShufflingFullHash { get; set; }
        public BinaryHexString ShufflingStateHash { get; set; }
        public ShufflingStage Stage { get; set; }
    }
}