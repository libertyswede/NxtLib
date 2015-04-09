using System.Collections.Generic;

namespace NxtLib.VotingSystem
{
    public class GetPollVotesReply : BaseReply
    {
        public List<PollVote> Votes { get; set; }
    }
}