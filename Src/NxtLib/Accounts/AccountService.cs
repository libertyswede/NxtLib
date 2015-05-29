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

        public async Task<AccountReply> GetAccount(string accountId, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null, bool? includeEffectiveBalance = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("includeLessors", includeLessors, queryParameters);
            AddToParametersIfHasValue("includeAssets", includeAssets, queryParameters);
            AddToParametersIfHasValue("includeCurrencies", includeCurrencies, queryParameters);
            AddToParametersIfHasValue("includeEffectiveBalance", includeEffectiveBalance, queryParameters);
            return await Get<AccountReply>("getAccount", queryParameters);
        }

        public async Task<AccountBlockCountReply> GetAccountBlockCount(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<AccountBlockCountReply>("getAccountBlockCount", queryParameters);
        }

        public async Task<AccountBlockIdsReply> GetAccountBlockIds(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlockIdsReply>("getAccountBlockIds", queryParameters);
        }

        public async Task<AccountBlocksReply<ulong>> GetAccountBlocks(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlocksReply<ulong>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountBlocksReply<Transaction>> GetAccountBlocksIncludeTransactions(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"includeTransactions", "true"}
            };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlocksReply<Transaction>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountIdReply> GetAccountId(AccountIdLocator locator)
        {
            return await Post<AccountIdReply>("getAccountId", locator.QueryParameters);
        }

        public async Task<AccountLessorsReply> GetAccountLessors(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            if (height.HasValue)
            {
                queryParameters.Add("height", height.Value.ToString());
            }
            return await Get<AccountLessorsReply>("getAccountLessors", queryParameters);
        }

        public async Task<AccountPublicKeyReply> GetAccountPublicKey(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<AccountPublicKeyReply>("getAccountPublicKey", queryParameters);
        }

        [Obsolete("This API is deprecated and will be removed in 1.6. It does not include the phased transactions that an account may have. To retrieve both phased and non-phased transactions, the new getBlockchainTransactions API must be used. Do not simply switch from getAccountTransactions to getBlockchainTransactions without a detailed understanding of how phased transactions work, and without being prepared to analyze them correctly.")]
        public async Task<AccountTransactionIdsReply> GetAccountTransactionIds(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phased = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, transactionType,
                firstIndex, lastIndex, numberOfConfirmations, withMessage, phased);
            return await Get<AccountTransactionIdsReply>("getAccountTransactionIds", queryParameters);
        }

        [Obsolete(@"This API is deprecated and will be removed in 1.6. It does not include the phased transactions that an account may have. To retrieve both phased and non-phased transactions, the new getBlockchainTransactions API must be used. Do not simply switch from getAccountTransactions to getBlockchainTransactions without a detailed understanding of how phased transactions work, and without being prepared to analyze them correctly.")]
        public async Task<TransactionListReply> GetAccountTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phased = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, transactionType,
                firstIndex, lastIndex, numberOfConfirmations, withMessage, phased);
            return await Get<TransactionListReply>("getAccountTransactions", queryParameters);
        }

        public async Task<BalanceReply> GetBalance(string accountId, bool? includeEffectiveBalance = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", accountId}};
            AddToParametersIfHasValue("includeEffectiveBalance", includeEffectiveBalance, queryParameters);
            return await Get<BalanceReply>("getBalance", queryParameters);
        }

        public async Task<TransactionListReply> GetBlockchainTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, transactionType,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            AddToParametersIfHasValue("phasedOnly", phasedOnly, queryParameters);
            AddToParametersIfHasValue("nonPhasedOnly", nonPhasedOnly, queryParameters);
            return await Get<TransactionListReply>("getBlockchainTransactions", queryParameters);
        }

        public async Task<GuaranteedBalanceReply> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            return await Get<GuaranteedBalanceReply>("getGuaranteedBalance", queryParameters);
        }

        public Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"query", query}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return Get<SearchAccountsReply>("searchAccounts", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, string recipient, Amount amount)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("recipient", recipient);
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

        private Dictionary<string, string> GenerateQueryParamsForAccountTransactions(string accountId, DateTime? timeStamp,
            TransactionSubType? transactionType, int? firstIndex, int? lastIndex, int? numberOfConfirmations, bool? withMessage, bool? phased)
        {

            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, transactionType,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            AddToParametersIfHasValue("phased", phased, queryParameters);
            return queryParameters;
        }

        private Dictionary<string, string> GenerateQueryParamsForAccountTransactions(string accountId, DateTime? timeStamp,
            TransactionSubType? transactionType, int? firstIndex, int? lastIndex, int? numberOfConfirmations, bool? withMessage)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            if (transactionType.HasValue)
            {
                queryParameters.Add("type", TransactionTypeMapper.GetMainTypeByte(transactionType.Value).ToString());
                queryParameters.Add("subtype", TransactionTypeMapper.GetSubTypeByte(transactionType.Value).ToString());
            }
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            AddToParametersIfHasValue("withMessage", withMessage, queryParameters);
            return queryParameters;
        }
    }
}
