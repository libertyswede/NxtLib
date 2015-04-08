using NxtLib.Internal;

namespace NxtLib.Voting_System
{
    public class VotingSystemService : BaseService, IVotingSystemService
    {
        public VotingSystemService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public VotingSystemService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public void CastVote()
        {
        }

        public void CreatePoll()
        {
        }

        public void GetPoll()
        {
        }

        public void GetPollResult()
        {
        }

        public void GetPollVote()
        {
        }

        public void GetPollVotes()
        {
        }

        public void GetPolls()
        {
        }

        public void SearchPolls()
        {
        }
    }
}
