using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.VotingSystem
{
    public class VotingSystemService : BaseService, IVotingSystemService
    {
        public VotingSystemService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public VotingSystemService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> CastVote(ulong pollId, Dictionary<int, int> votes,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            foreach (var vote in votes)
            {
                queryParameters.Add("vote" + vote.Key.ToString().PadLeft(2, '0'), vote.Value.ToString());
            }
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("castVote", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(CreatePollParameters createPollParameters,
            CreateTransactionParameters parameters)
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
            queryParameters.AddIfHasValue("minBalance", createPollParameters.MinBalance);
            queryParameters.AddIfHasValue("minBalanceModel", createPollParameters.MinBalanceModel);
            queryParameters.AddIfHasValue("holding", createPollParameters.HoldingId);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }

        public async Task<GetPollReply> GetPoll(ulong pollId, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollReply>("getPoll", queryParameters);
        }

        public async Task<GetPollResultReply> GetPollResult(ulong pollId, VotingModel? votingModel = null,
            ulong? holdingId = null, long? minBalance = null, MinBalanceModel? minBalanceModel = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            queryParameters.AddIfHasValue("votingModel", votingModel.HasValue ? (int?)votingModel.Value : null);
            queryParameters.AddIfHasValue("holding", holdingId);
            queryParameters.AddIfHasValue(nameof(minBalance), minBalance);
            queryParameters.AddIfHasValue(nameof(minBalanceModel), minBalanceModel);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollResultReply>("getPollResult", queryParameters);
        }

        public async Task<GetPollsReply> GetPolls(Account account = null, int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeFinished = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(includeFinished), includeFinished);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollsReply>("getPolls", queryParameters);
        }

        public async Task<GetPollVoteReply> GetPollVote(ulong pollId, Account account, bool? includeWeights = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}, {nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(includeWeights), includeWeights);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollVoteReply>("getPollVote", queryParameters);
        }

        public async Task<GetPollVotesReply> GetPollVotes(ulong pollId, int? firstIndex = null, int? lastIndex = null,
            bool? includeWeights = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"poll", pollId.ToString()}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeWeights), includeWeights);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollVotesReply>("getPollVotes", queryParameters);
        }

        public async Task<GetPollsReply> SearchPolls(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeFinished = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(query), query}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(includeFinished), includeFinished);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetPollsReply>("searchPolls", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options, CreateTransactionParameters parameters, long? minBalance = null,
            MinBalanceModel? minBalanceModel = null, ulong? holdingId = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {nameof(name), name},
                {nameof(description), description},
                {nameof(finishHeight), finishHeight.ToString()},
                {nameof(votingModel), ((int) votingModel).ToString()},
                {nameof(minNumberOfOptions), minNumberOfOptions.ToString()},
                {nameof(maxNumberOfOptions), maxNumberOfOptions.ToString()},
                {nameof(minRangeValue), minRangeValue.ToString()},
                {nameof(maxRangeValue), maxRangeValue.ToString()}
            };
            for (var i = 0; i < options.Count; i++)
            {
                queryParameters.Add("option" + i.ToString().PadLeft(2, '0'), options[i]);
            }
            queryParameters.AddIfHasValue(nameof(minBalance), minBalance);
            queryParameters.AddIfHasValue(nameof(minBalanceModel), minBalanceModel);
            queryParameters.AddIfHasValue("holding", holdingId);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }
    }
}