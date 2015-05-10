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
            : base(new DateTimeConverter(), baseUrl)
        {
        }

        public PhasingService(IDateTimeConverter dateTimeConverter) : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> ApproveTransaction(List<BinaryHexString> transactionFullHashes,
            CreateTransactionParameters parameters, string revealedSecret = null, bool? revealedSecretIsText = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {"transactionFullHash", transactionFullHashes.Select(hash => hash.ToString()).ToList()}
            };
            if (!string.IsNullOrEmpty(revealedSecret))
            {
                queryParameters.Add("revealedSecret", new List<string> {revealedSecret});
            }
            if (revealedSecretIsText.HasValue)
            {
                queryParameters.Add("revealedSecretIsText", new List<string> {revealedSecretIsText.Value.ToString()});
            }
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("approveTransaction", queryParameters);
        }

        public async Task<AccountPhasedTransactionCountReply> GetAccountPhasedTransactionCount(string accountId)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            return await Get<AccountPhasedTransactionCountReply>("getAccountPhasedTransactionCount", queryParameters);
        }

        public async Task<TransactionListReply> GetAccountPhasedTransactions(string accountId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<TransactionListReply>("getAccountPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetAssetPhasedTransactions(ulong assetId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "asset", assetId.ToString() } };
            AddToParametersIfHasValue("account", accountId, queryParameters);
            AddToParametersIfHasValue("withoutWhitelist", withoutWhitelist, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<TransactionListReply>("getAssetPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetCurrencyPhasedTransactions(ulong currencyId, string accountId = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"currency", currencyId.ToString()}};
            AddToParametersIfHasValue("account", accountId, queryParameters);
            AddToParametersIfHasValue("withoutWhitelist", withoutWhitelist, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<TransactionListReply>("getCurrencyPhasedTransactions", queryParameters);
        }

        public async Task<PhasingPollReply> GetPhasingPoll(ulong transactionId, bool? countVotes = false)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("countVotes", countVotes, queryParameters);
            return await Get<PhasingPollReply>("getPhasingPoll", queryParameters);
        }

        public async Task<PhasingPollVoteReply> GetPhasingPollVote(ulong transactionId, string accountId)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"account", accountId}
            };
            return await Get<PhasingPollVoteReply>("getPhasingPollVote", queryParameters);
        }

        public async Task<PhasingPollVotesReply> GetPhasingPollVotes(ulong transactionId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()}
            };
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<PhasingPollVotesReply>("getPhasingPollVotes", queryParameters);
        }

        public async Task<PhasingPollsReply> GetPhasingPolls(List<ulong> transactionIds, bool? countVotes = false)
        {
            var queryParameters = new Dictionary<string, List<string>>()
            {
                {"transaction", transactionIds.Select(id => id.ToString()).ToList()}
            };
            if (countVotes.HasValue)
            {
                queryParameters.Add("countVotes", new List<string>{countVotes.Value.ToString()});
            }
            return await Get<PhasingPollsReply>("getPhasingPolls", queryParameters);
        }

        public async Task<TransactionListReply> GetVoterPhasedTransactions(string accountId, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<TransactionListReply>("getVoterPhasedTransactions", queryParameters);
        }
    }
}
