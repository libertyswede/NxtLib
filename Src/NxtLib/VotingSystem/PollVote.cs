using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.VotingSystem
{
    public class PollVote
    {
        [JsonProperty(PropertyName = Parameters.Transaction)]
        public ulong TransactionId { get; set; }

        [JsonProperty(PropertyName = Parameters.Voter)]
        public ulong VoterId { get; set; }
        public string VoterRs { get; set; }
        public List<int> Votes { get; set; }
    }
}