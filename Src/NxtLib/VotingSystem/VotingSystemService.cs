using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.VotingSystem
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

        public async Task<TransactionCreatedReply> CastVote(ulong pollId, Dictionary<int, int> votes, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            foreach (var vote in votes)
            {
                queryParameters.Add("vote" + vote.Key.ToString().PadLeft(2, '0'), vote.Value.ToString());
            }
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("castVote", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options, CreateTransactionParameters parameters, ulong? minBalance = null,
            MinBalanceModel? minBalanceModel = null, ulong? holdingId = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"name", name},
                {"description", description},
                {"finishHeight", finishHeight.ToString()},
                {"votingModel", ((int) votingModel).ToString()},
                {"minNumberOfOptions", minNumberOfOptions.ToString()},
                {"maxNumberOfOptions", maxNumberOfOptions.ToString()},
                {"minRangeValue", minRangeValue.ToString()},
                {"maxRangeValue", maxRangeValue.ToString()}
            };
            for (var i = 0; i < options.Count; i++)
            {
                queryParameters.Add("option" + i.ToString().PadLeft(2, '0'), options[i]);
            }
            AddToParametersIfHasValue("minBalance", minBalance, queryParameters);
            AddToParametersIfHasValue("minBalanceModel", minBalanceModel.HasValue ? (int?) minBalanceModel.Value : null,
                queryParameters);
            AddToParametersIfHasValue("holding", holdingId, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }

        public async Task<GetPollReply> GetPoll(ulong pollId)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            return await Get<GetPollReply>("getPoll", queryParameters);
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

    public enum MinBalanceModel
    {
        Nxt = 0,
        AssetBalance = 1,
        CurrencyBalance = 2
    }

    public enum VotingModel
    {
        OneVotePerAccount = 0,
        VoteByNxtBalance = 1,
        VoteByAsset = 2,
        VoteByCurrency = 3
    }
}
