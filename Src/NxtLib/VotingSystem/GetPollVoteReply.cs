namespace NxtLib.VotingSystem
{
    public class GetPollVoteReply : PollVote, IBaseReply
    {
        public string RawJsonReply { get; set; }
        public int RequestProcessingTime { get; set; }
        public string RequestUri { get; set; }
    }
}