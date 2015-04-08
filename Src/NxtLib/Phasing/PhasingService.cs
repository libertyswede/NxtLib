using NxtLib.Internal;

namespace NxtLib.Phasing
{
    public class PhasingService : BaseService, IPhasingService
    {
        public PhasingService(string baseUrl = DefaultBaseUrl) : base(new DateTimeConverter(), baseUrl)
        {
        }

        public PhasingService(IDateTimeConverter dateTimeConverter) : base(dateTimeConverter)
        {
        }

        public void ApproveTransaction()
        {
        }

        public void GetAccountPhasedTransactionCount()
        {
        }

        public void GetAccountPhasedTransactions()
        {
        }

        public void GetAssetPhasedTransactions()
        {
        }

        public void GetCurrencyPhasedTransactions()
        {
        }

        public void GetPhasingPoll()
        {
        }

        public void GetPhasingPollVote()
        {
        }

        public void GetPhasingPollVotes()
        {
        }

        public void GetPhasingPolls()
        {
        }

        public void GetVoterPhasedTransactions()
        {
        }
    }
}
