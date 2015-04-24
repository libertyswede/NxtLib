using System.Collections.Generic;
using Newtonsoft.Json;

namespace NxtLib.VotingSystem
{
    public class PollVote
    {
        [JsonProperty(PropertyName = "transaction")]
        public ulong TransactionId { get; set; }

        [JsonProperty(PropertyName = "voter")]
        public ulong VoterId { get; set; }
        public string VoterRs { get; set; }
        public List<int> Votes { get; set; }
    }
}