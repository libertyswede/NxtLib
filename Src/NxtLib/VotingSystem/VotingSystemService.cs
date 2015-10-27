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
            : base(baseAddress)
        {
        }

        public async Task<TransactionCreatedReply> CastVote(ulong pollId, Dictionary<int, int> votes,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Poll, pollId.ToString()}};
            foreach (var vote in votes)
            {
                queryParameters.Add(Parameters.Vote + vote.Key.ToString().PadLeft(2, '0'), vote.Value.ToString());
            }
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("castVote", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(CreatePollParameters createPollParameters,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Name, createPollParameters.Name},
                {Parameters.Description, createPollParameters.Description},
                {Parameters.FinishHeight, createPollParameters.FinishHeight.ToString()},
                {Parameters.VotingModel, ((int) createPollParameters.VotingModel).ToString()},
                {Parameters.MinNumberOfOptions, createPollParameters.MinNumberOfOptions.ToString()},
                {Parameters.MaxNumberOfOptions, createPollParameters.MaxNumberOfOptions.ToString()},
                {Parameters.MinRangeValue, createPollParameters.MinRangeValue.ToString()},
                {Parameters.MaxRangeValue, createPollParameters.MaxRangeValue.ToString()}
            };
            for (var i = 0; i < createPollParameters.Options.Count; i++)
            {
                queryParameters.Add(Parameters.Option + i.ToString().PadLeft(2, '0'), createPollParameters.Options[i]);
            }
            queryParameters.AddIfHasValue(Parameters.MinBalance, createPollParameters.MinBalance);
            queryParameters.AddIfHasValue(Parameters.MinBalanceModel, createPollParameters.MinBalanceModel);
            queryParameters.AddIfHasValue(Parameters.Holding, createPollParameters.HoldingId);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }

        public async Task<GetPollReply> GetPoll(ulong pollId, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Poll, pollId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollReply>("getPoll", queryParameters);
        }

        public async Task<GetPollResultReply> GetPollResult(ulong pollId, VotingModel? votingModel = null,
            ulong? holdingId = null, long? minBalance = null, MinBalanceModel? minBalanceModel = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Poll, pollId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.VotingModel, votingModel.HasValue ? (int?)votingModel.Value : null);
            queryParameters.AddIfHasValue(Parameters.Holding, holdingId);
            queryParameters.AddIfHasValue(Parameters.MinBalance, minBalance);
            queryParameters.AddIfHasValue(Parameters.MinBalanceModel, minBalanceModel);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollResultReply>("getPollResult", queryParameters);
        }

        public async Task<GetPollsReply> GetPolls(Account account = null, int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeFinished = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollsReply>("getPolls", queryParameters);
        }

        public async Task<GetPollVoteReply> GetPollVote(ulong pollId, Account account, bool? includeWeights = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Poll, pollId.ToString()}, {Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeWeights, includeWeights);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollVoteReply>("getPollVote", queryParameters);
        }

        public async Task<GetPollVotesReply> GetPollVotes(ulong pollId, int? firstIndex = null, int? lastIndex = null,
            bool? includeWeights = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Poll, pollId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeWeights, includeWeights);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollVotesReply>("getPollVotes", queryParameters);
        }

        public async Task<GetPollsReply> SearchPolls(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeFinished = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Query, query}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetPollsReply>("searchPolls", queryParameters);
        }

        public async Task<TransactionCreatedReply> CreatePoll(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options, CreateTransactionParameters parameters, long? minBalance = null,
            MinBalanceModel? minBalanceModel = null, ulong? holdingId = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Name, name},
                {Parameters.Description, description},
                {Parameters.FinishHeight, finishHeight.ToString()},
                {Parameters.VotingModel, ((int) votingModel).ToString()},
                {Parameters.MinNumberOfOptions, minNumberOfOptions.ToString()},
                {Parameters.MaxNumberOfOptions, maxNumberOfOptions.ToString()},
                {Parameters.MinRangeValue, minRangeValue.ToString()},
                {Parameters.MaxRangeValue, maxRangeValue.ToString()}
            };
            for (var i = 0; i < options.Count; i++)
            {
                queryParameters.Add(Parameters.Option + i.ToString().PadLeft(2, '0'), options[i]);
            }
            queryParameters.AddIfHasValue(Parameters.MinBalance, minBalance);
            queryParameters.AddIfHasValue(Parameters.MinBalanceModel, minBalanceModel);
            queryParameters.AddIfHasValue(Parameters.Holding, holdingId);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("createPoll", queryParameters);
        }
    }
}