namespace NxtLib.Phasing
{
    public class PhasingPollVoteReply : PhasingPollVote, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}