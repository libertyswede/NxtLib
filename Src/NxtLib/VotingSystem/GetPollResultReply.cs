using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.VotingSystem
{
    public class GetPollResultReply : BaseReply
    {
        public int? Decimals { get; set; }
        public bool Finished { get; set; }

        [JsonProperty(PropertyName = "holding")]
        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public ulong HoldingId { get; set; }
        public MinBalanceModel MinBalanceModel { get; set; }

        [JsonConverter(typeof(StringToIntegralTypeConverter))]
        public long MinBalance { get; set; }
        public List<string> Options { get; set; }

        [JsonProperty(PropertyName = "poll")]
        public ulong PollId { get; set; }
        public List<PollResult> Results { get; set; }
        public VotingModel? VotingModel { get; set; }
    }
}