using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Shuffling
{
    public class Shuffler
    {
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Account)]
        public ulong AccountId { get; set; }
        public string AccountRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Recipient)]
        public ulong RecipientId { get; set; }
        public string RecipientRs { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        [JsonProperty(PropertyName = Parameters.Shuffling)]
        public ulong ShufflingId { get; set; }

        public BinaryHexString ShufflingFullHash { get; set; }
    }
}