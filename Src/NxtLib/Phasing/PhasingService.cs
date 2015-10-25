using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Phasing
{
    public class PhasingService : BaseService, IPhasingService
    {
        public PhasingService(string baseUrl = Constants.DefaultNxtUrl)
            : base(baseUrl)
        {
        }

        public async Task<TransactionCreatedReply> ApproveTransaction(
            IEnumerable<BinaryHexString> transactionFullHashes, CreateTransactionParameters parameters,
            string revealedSecret = null, bool? revealedSecretIsText = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"transactionFullHash", transactionFullHashes.Select(hash => hash.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(nameof(revealedSecret), revealedSecret);
            queryParameters.AddIfHasValue(nameof(revealedSecretIsText), revealedSecretIsText);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("approveTransaction", queryParameters);
        }

        public async Task<AccountPhasedTransactionCountReply> GetAccountPhasedTransactionCount(Account account,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<AccountPhasedTransactionCountReply>("getAccountPhasedTransactionCount", queryParameters);
        }

        public async Task<TransactionListReply> GetAccountPhasedTransactions(Account account, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionListReply>("getAccountPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetAssetPhasedTransactions(ulong assetId, Account account = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"asset", assetId.ToString()}};
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(withoutWhitelist), withoutWhitelist);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionListReply>("getAssetPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetCurrencyPhasedTransactions(ulong currencyId, Account account = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            queryParameters.AddIfHasValue(nameof(account), account);
            queryParameters.AddIfHasValue(nameof(withoutWhitelist), withoutWhitelist);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionListReply>("getCurrencyPhasedTransactions", queryParameters);
        }

        public async Task<PhasingPollReply> GetPhasingPoll(ulong transactionId, bool? countVotes = false,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(countVotes), countVotes);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PhasingPollReply>("getPhasingPoll", queryParameters);
        }

        public async Task<PhasingPollVoteReply> GetPhasingPollVote(ulong transactionId, Account account,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {nameof(account), account.AccountId.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PhasingPollVoteReply>("getPhasingPollVote", queryParameters);
        }

        public async Task<PhasingPollVotesReply> GetPhasingPollVotes(ulong transactionId, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()}
            };
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PhasingPollVotesReply>("getPhasingPollVotes", queryParameters);
        }

        public async Task<PhasingPollsReply> GetPhasingPolls(IEnumerable<ulong> transactionIds, bool? countVotes = false,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"transaction", transactionIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(nameof(countVotes), countVotes);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<PhasingPollsReply>("getPhasingPolls", queryParameters);
        }

        public async Task<TransactionListReply> GetVoterPhasedTransactions(Account account, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionListReply>("getVoterPhasedTransactions", queryParameters);
        }
    }
}