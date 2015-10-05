using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Phasing
{
    public interface IPhasingService
    {
        Task<TransactionCreatedReply> ApproveTransaction(IEnumerable<BinaryHexString> transactionFullHashes,
            CreateTransactionParameters parameters, string revealedSecret = null, bool? revealedSecretIsText = null);

        Task<AccountPhasedTransactionCountReply> GetAccountPhasedTransactionCount(string accountId,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionListReply> GetAccountPhasedTransactions(string accountId, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionListReply> GetAssetPhasedTransactions(ulong assetId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionListReply> GetCurrencyPhasedTransactions(ulong currencyId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<PhasingPollReply> GetPhasingPoll(ulong transactionId, bool? countVotes = false, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<PhasingPollVoteReply> GetPhasingPollVote(ulong transactionId, string accountId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<PhasingPollVotesReply> GetPhasingPollVotes(ulong transactionId, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<PhasingPollsReply> GetPhasingPolls(IEnumerable<ulong> transactionIds, bool? countVotes = false,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionListReply> GetVoterPhasedTransactions(string accountId, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);
    }
}