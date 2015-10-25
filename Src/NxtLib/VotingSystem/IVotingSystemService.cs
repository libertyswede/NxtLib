using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.VotingSystem
{
    public interface IVotingSystemService
    {
        Task<TransactionCreatedReply> CastVote(ulong pollId, Dictionary<int, int> votes,
            CreateTransactionParameters parameters);

        Task<TransactionCreatedReply> CreatePoll(CreatePollParameters createPollParameters,
            CreateTransactionParameters parameters);

        Task<GetPollReply> GetPoll(ulong pollId, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetPollResultReply> GetPollResult(ulong pollId, VotingModel? votingModel = null,
            ulong? holdingId = null, long? minBalance = null, MinBalanceModel? minBalanceModel = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetPollsReply> GetPolls(Account account = null, int? firstIndex = null, int? lastIndex = null, DateTime? timestamp = null, bool? includeFinished = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetPollVoteReply> GetPollVote(ulong pollId, Account account, bool? includeWeights = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetPollVotesReply> GetPollVotes(ulong pollId, int? firstIndex = null, int? lastIndex = null,
            bool? includeWeights = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetPollsReply> SearchPolls(string query, int? firstIndex = null, int? lastIndex = null,
            bool? includeFinished = null, ulong? requireBlock = null, ulong? requireLastBlock = null);
    }
}