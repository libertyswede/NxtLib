using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Accounts
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AccountService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<AccountReply> GetAccount(string accountId, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("includeLessors", includeLessors, queryParameters);
            AddToParametersIfHasValue("includeAssets", includeAssets, queryParameters);
            AddToParametersIfHasValue("includeCurrencies", includeCurrencies, queryParameters);
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

        public async Task<AccountTransactionIdsReply> GetAccountTransactionIds(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, type, subtype,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            return await Get<AccountTransactionIdsReply>("getAccountTransactionIds", queryParameters);
        }

        public async Task<AccountTransactionsReply> GetAccountTransactions(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, type, subtype,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            return await Get<AccountTransactionsReply>("getAccountTransactions", queryParameters);
        }

        public async Task<BalanceReply> GetBalance(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<BalanceReply>("getBalance", queryParameters);
        }

        public async Task<GuaranteedBalanceReply> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            return await Get<GuaranteedBalanceReply>("getGuaranteedBalance", queryParameters);
        }

        public async Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedTransactionIdsResply>("getUnconfirmedTransactionIds", queryParameters);
        }

        public async Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedTransactionsReply>("getUnconfirmedTransactions", queryParameters);
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

        private Dictionary<string, string> GenerateQueryParamsForAccountTransactions(string accountId, DateTime? timeStamp, byte? type,
            byte? subtype, int? firstIndex, int? lastIndex, int? numberOfConfirmations, bool? withMessage)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("type", type, queryParameters);
            AddToParametersIfHasValue("subtype", subtype, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            AddToParametersIfHasValue("withMessage", withMessage, queryParameters);
            return queryParameters;
        }
    }
}
