using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Accounts
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AccountService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<AccountReply> GetAccount(Account account, bool? includeLessors = null,
            bool? includeAssets = null, bool? includeCurrencies = null, bool? includeEffectiveBalance = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("includeLessors", includeLessors, queryParameters);
            AddToParametersIfHasValue("includeAssets", includeAssets, queryParameters);
            AddToParametersIfHasValue("includeCurrencies", includeCurrencies, queryParameters);
            AddToParametersIfHasValue("includeEffectiveBalance", includeEffectiveBalance, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<AccountReply>("getAccount", queryParameters);
        }

        public async Task<AccountBlockCountReply> GetAccountBlockCount(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<AccountBlockCountReply>("getAccountBlockCount", queryParameters);
        }

        public async Task<AccountBlockIdsReply> GetAccountBlockIds(Account account, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<AccountBlockIdsReply>("getAccountBlockIds", queryParameters);
        }

        public async Task<AccountBlocksReply<ulong>> GetAccountBlocks(Account account, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = BuildQueryParametersForGetAccountBlocks(account, false, timeStamp, firstIndex,
                lastIndex, requireBlock, requireLastBlock);
            return await Get<AccountBlocksReply<ulong>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountBlocksReply<Transaction>> GetAccountBlocksIncludeTransactions(Account account,
            DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = BuildQueryParametersForGetAccountBlocks(account, true, timeStamp, firstIndex,
                lastIndex, requireBlock, requireLastBlock);
            return await Get<AccountBlocksReply<Transaction>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountIdReply> GetAccountId(AccountIdLocator locator)
        {
            return await Post<AccountIdReply>("getAccountId", locator.QueryParameters);
        }

        public async Task<GetAccountLedgerReply> GetAccountLedger(Account account, int? firstIndex = null,
            int? lastIndex = null, string eventType = null, ulong? @event = null, string holdingType = null,
            ulong? holding = null, bool? includeTransactions = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("eventType", eventType, queryParameters);
            AddToParametersIfHasValue("event", @event, queryParameters);
            AddToParametersIfHasValue("holdingType", holdingType, queryParameters);
            AddToParametersIfHasValue("holding", holding, queryParameters);
            AddToParametersIfHasValue("includeTransactions", includeTransactions, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GetAccountLedgerReply>("getAccountLedger", queryParameters);
        }

        public async Task<GetAccountLedgerEntryReply> GetAccountLedgerEntry(int ledgerId,
            bool? includeTransaction = null)
        {
            var queryParameters = new Dictionary<string, string> {{"ledgerId", ledgerId.ToString()}};
            AddToParametersIfHasValue("includeTransaction", includeTransaction, queryParameters);
            return await Get<GetAccountLedgerEntryReply>("getAccountLedgerEntry", queryParameters);
        }

        public async Task<AccountLessorsReply> GetAccountLessors(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("height", height, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<AccountLessorsReply>("getAccountLessors", queryParameters);
        }

        public async Task<AccountPublicKeyReply> GetAccountPublicKey(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<AccountPublicKeyReply>("getAccountPublicKey", queryParameters);
        }

        public async Task<BalanceReply> GetBalance(Account account, bool? includeEffectiveBalance = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("includeEffectiveBalance", includeEffectiveBalance, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<BalanceReply>("getBalance", queryParameters);
        }

        public async Task<GuaranteedBalanceReply> GetGuaranteedBalance(Account account,
            int? numberOfConfirmations = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GuaranteedBalanceReply>("getGuaranteedBalance", queryParameters);
        }

        public Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"query", query}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return Get<SearchAccountsReply>("searchAccounts", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, Account recipient,
            Amount amount)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("recipient", recipient.AccountId.ToString());
            queryParameters.Add("amountNQT", amount.Nqt.ToString());
            return await Post<TransactionCreatedReply>("sendMoney", queryParameters);
        }

        public async Task<TransactionCreatedReply> SetAccountInfo(CreateTransactionParameters parameters, string name,
            string description)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("name", name);
            queryParameters.Add("description", description);
            return await Post<TransactionCreatedReply>("setAccountInfo", queryParameters);
        }

        private Dictionary<string, string> BuildQueryParametersForGetAccountBlocks(Account account,
            bool includeTransactions, DateTime? timeStamp, int? firstIndex,
            int? lastIndex, ulong? requireBlock, ulong? requireLastBlock)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", account.AccountId.ToString()},
                {"includeTransactions", includeTransactions.ToString()}
            };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return queryParameters;
        }
    }
}