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
                {Parameters.TransactionFullHash, transactionFullHashes.Select(hash => hash.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.RevealedSecret, revealedSecret);
            queryParameters.AddIfHasValue(Parameters.RevealedSecretIsText, revealedSecretIsText);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("approveTransaction", queryParameters);
        }

        public async Task<AccountPhasedTransactionCountReply> GetAccountPhasedTransactionCount(Account account,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountPhasedTransactionCountReply>("getAccountPhasedTransactionCount", queryParameters);
        }

        public async Task<TransactionListReply> GetAccountPhasedTransactions(Account account, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getAccountPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetAssetPhasedTransactions(ulong assetId, Account account = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Asset, assetId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.WithoutWhitelist, withoutWhitelist);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getAssetPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetCurrencyPhasedTransactions(ulong currencyId, Account account = null,
            bool? withoutWhitelist = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Currency, currencyId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.WithoutWhitelist, withoutWhitelist);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getCurrencyPhasedTransactions", queryParameters);
        }

        public async Task<TransactionListReply> GetLinkedPhasedTransactions(BinaryHexString linkedFullHash,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.LinkedFullHash, linkedFullHash.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getLinkedPhasedTransactions", queryParameters);
        }

        public async Task<PhasingPollReply> GetPhasingPoll(ulong transactionId, bool? countVotes = false,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.CountVotes, countVotes);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PhasingPollReply>("getPhasingPoll", queryParameters);
        }

        public async Task<PhasingPollVoteReply> GetPhasingPollVote(ulong transactionId, Account account,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Transaction, transactionId.ToString()},
                {Parameters.Account, account.AccountId.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PhasingPollVoteReply>("getPhasingPollVote", queryParameters);
        }

        public async Task<PhasingPollVotesReply> GetPhasingPollVotes(ulong transactionId, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Transaction, transactionId.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PhasingPollVotesReply>("getPhasingPollVotes", queryParameters);
        }

        public async Task<PhasingPollsReply> GetPhasingPolls(IEnumerable<ulong> transactionIds, bool? countVotes = false,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>
            {
                {Parameters.Transaction, transactionIds.Select(id => id.ToString()).ToList()}
            };
            queryParameters.AddIfHasValue(Parameters.CountVotes, countVotes);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<PhasingPollsReply>("getPhasingPolls", queryParameters);
        }

        public async Task<TransactionListReply> GetVoterPhasedTransactions(Account account, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getVoterPhasedTransactions", queryParameters);
        }
    }
}