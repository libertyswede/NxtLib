using System.Collections.Generic;

namespace NxtLib.Phasing
{
    public class PhasingPollsReply : BaseReply
    {
        public List<PhasingPoll> Polls { get; set; }
    }
}