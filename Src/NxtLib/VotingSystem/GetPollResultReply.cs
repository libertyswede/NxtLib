using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.VotingSystem
{
    public class GetPollResultReply : BaseReply
    {
        public bool Finished { get; set; }
        public List<string> Options { get; set; }

        [JsonProperty(PropertyName = "poll")]
        public ulong PollId { get; set; }
        public List<PollResult> Results { get; set; }
    }
}