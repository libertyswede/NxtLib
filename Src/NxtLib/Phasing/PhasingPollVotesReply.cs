using System.Collections.Generic;

namespace NxtLib.Phasing
{
    public class PhasingPollVotesReply : BaseReply
    {
        public List<PhasingPollVote> Votes { get; set; }
    }
}