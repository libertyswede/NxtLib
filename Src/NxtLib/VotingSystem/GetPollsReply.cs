using System.Collections.Generic;

namespace NxtLib.VotingSystem
{
    public class GetPollsReply : BaseReply
    {
        public List<Poll> Polls { get; set; }
    }
}