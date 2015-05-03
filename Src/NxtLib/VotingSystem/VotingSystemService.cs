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

        public async Task<TransactionCreatedReply> CreatePoll(CreatePollParameters createPollParameters, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"name", createPollParameters.Name},
                {"description", createPollParameters.Description},
                {"finishHeight", createPollParameters.FinishHeight.ToString()},
                {"votingModel", ((int) createPollParameters.VotingModel).ToString()},
                {"minNumberOfOptions", createPollParameters.MinNumberOfOptions.ToString()},
                {"maxNumberOfOptions", createPollParameters.MaxNumberOfOptions.ToString()},
                {"minRangeValue", createPollParameters.MinRangeValue.ToString()},
                {"maxRangeValue", createPollParameters.MaxRangeValue.ToString()}
            };
            for (var i = 0; i < createPollParameters.Options.Count; i++)
            {
                queryParameters.Add("option" + i.ToString().PadLeft(2, '0'), createPollParameters.Options[i]);
            }
            AddToParametersIfHasValue("minBalance", createPollParameters.MinBalance, queryParameters);
            AddToParametersIfHasValue("minBalanceModel",
                createPollParameters.MinBalanceModel.HasValue ? (int?) createPollParameters.MinBalanceModel.Value : null,
                queryParameters);
            AddToParametersIfHasValue("holding", createPollParameters.HoldingId, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options, CreateTransactionParameters parameters, long? minBalance = null,
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

        public async Task<GetPollResultReply> GetPollResult(ulong pollId, VotingModel? votingModel = null,
            ulong? holdingId = null, long? minBalance = null, MinBalanceModel? minBalanceModel = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            AddToParametersIfHasValue("votingModel", votingModel.HasValue ? (int?)votingModel.Value : null,
                queryParameters);
            AddToParametersIfHasValue("holding", holdingId, queryParameters);
            AddToParametersIfHasValue("minBalance", minBalance, queryParameters);
            AddToParametersIfHasValue("minBalanceModel", minBalanceModel.HasValue ? (int?)minBalanceModel.Value : null,
                queryParameters);
            return await Get<GetPollResultReply>("getPollResult", queryParameters);
        }

        public async Task<GetPollsReply> GetPolls(string accountId = null, int? firstIndex = null, int? lastIndex = null, bool? includeFinished = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeFinished", includeFinished, queryParameters);
            return await Get<GetPollsReply>("getPolls", queryParameters);
        }

        public async Task<GetPollVoteReply> GetPollVote(ulong pollId, string accountId)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}, {"account", accountId}};
            return await Get<GetPollVoteReply>("getPollVote", queryParameters);
        }

        public async Task<GetPollVotesReply> GetPollVotes(ulong pollId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<GetPollVotesReply>("getPollVotes", queryParameters);
        }

        public async Task<GetPollsReply> SearchPolls(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeFinished = null)
        {
            var queryParameters = new Dictionary<string, string> {{"query", query}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeFinished", includeFinished, queryParameters);
            return await Get<GetPollsReply>("searchPolls", queryParameters);
        }
    }
}
