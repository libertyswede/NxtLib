using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.VotingSystem
{
    public interface IVotingSystemService
    {
        Task<TransactionCreatedReply> CastVote(ulong pollId, Dictionary<int, int> votes, CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CreatePoll(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options, CreateTransactionParameters parameters, long? minBalance = null,
            MinBalanceModel? minBalanceModel = null, ulong? holdingId = null);

        Task<GetPollReply> GetPoll(ulong pollId);

        Task<GetPollResultReply> GetPollResult(ulong pollId, VotingModel? votingModel,
            ulong? holdingId = null, long? minBalance = null, MinBalanceModel? minBalanceModel = null);

        Task<GetPollsReply> GetPolls(string accountId = null, int? firstIndex = null, int? lastIndex = null, bool? includeFinished = null);
        Task<GetPollVoteReply> GetPollVote(ulong pollId, string accountId);
        Task<GetPollVotesReply> GetPollVotes(ulong pollId, int? firstIndex = null, int? lastIndex = null);

        Task<GetPollsReply> SearchPolls(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeFinished = null);
    }
}