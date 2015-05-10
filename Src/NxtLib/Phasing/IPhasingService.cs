using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Phasing
{
    public interface IPhasingService
    {
        Task<TransactionCreatedReply> ApproveTransaction(List<BinaryHexString> transactionFullHashes,
            CreateTransactionParameters parameters, string revealedSecret = null, bool? revealedSecretIsText = null);

        Task<AccountPhasedTransactionCountReply> GetAccountPhasedTransactionCount(string accountId);
        Task<TransactionListReply> GetAccountPhasedTransactions(string accountId, int? firstIndex = null, int? lastIndex = null);

        Task<TransactionListReply> GetAssetPhasedTransactions(ulong assetId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null);

        Task<TransactionListReply> GetCurrencyPhasedTransactions(ulong currencyId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null);

        Task<PhasingPollReply> GetPhasingPoll(ulong transactionId, bool? countVotes = false);
        Task<PhasingPollVoteReply> GetPhasingPollVote(ulong transactionId, string accountId);
        Task<PhasingPollVotesReply> GetPhasingPollVotes(ulong transactionId, int? firstIndex = null, int? lastIndex = null);
        Task<PhasingPollsReply> GetPhasingPolls(List<ulong> transactionIds, bool? countVotes = false);
        Task<TransactionListReply> GetVoterPhasedTransactions(string accountId, int? firstIndex = null, int? lastIndex = null);
    }
}