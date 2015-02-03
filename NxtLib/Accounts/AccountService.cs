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

        public async Task<AccountBlockCount> GetAccountBlockCount(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<AccountBlockCount>("getAccountBlockCount", queryParameters);
        }

        public async Task<AccountBlockIds> GetAccountBlockIds(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlockIds>("getAccountBlockIds", queryParameters);
        }

        public async Task<AccountBlocks<ulong>> GetAccountBlocks(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlocks<ulong>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountBlocks<Transaction>> GetAccountBlocksIncludeTransactions(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"account", accountId},
                {"includeTransactions", "true"}
            };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AccountBlocks<Transaction>>("getAccountBlocks", queryParameters);
        }

        public async Task<AccountId> GetAccountId(AccountIdLocator locator)
        {
            return await Post<AccountId>("getAccountId", locator.QueryParameters);
        }

        public async Task<AccountLessors> GetAccountLessors(string accountId, int? height = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            if (height.HasValue)
            {
                queryParameters.Add("height", height.Value.ToString());
            }
            return await Get<AccountLessors>("getAccountLessors", queryParameters);
        }

        public async Task<AccountPublicKey> GetAccountPublicKey(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<AccountPublicKey>("getAccountPublicKey", queryParameters);
        }

        public async Task<AccountTransactionIds> GetAccountTransactionIds(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, type, subtype,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            return await Get<AccountTransactionIds>("getAccountTransactionIds", queryParameters);
        }

        public async Task<AccountTransactions> GetAccountTransactions(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null)
        {
            var queryParameters = GenerateQueryParamsForAccountTransactions(accountId, timeStamp, type, subtype,
                firstIndex, lastIndex, numberOfConfirmations, withMessage);
            return await Get<AccountTransactions>("getAccountTransactions", queryParameters);
        }

        public async Task<AccountBalance> GetBalance(string accountId)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            return await Get<AccountBalance>("getBalance", queryParameters);
        }

        public async Task<AccountGuaranteedBalance> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            return await Get<AccountGuaranteedBalance>("getGuaranteedBalance", queryParameters);
        }

        public async Task<UnconfirmedAccountTransactionIds> GetUnconfirmedTransactionIds(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedAccountTransactionIds>("getUnconfirmedTransactionIds", queryParameters);
        }

        public async Task<UnconfirmedAccountTransactions> GetUnconfirmedTransactions(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedAccountTransactions>("getUnconfirmedTransactions", queryParameters);
        }

        public async Task<TransactionCreated> SendMoney(CreateTransactionParameters parameters, string recipient, Amount amount)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("recipient", recipient);
            queryParameters.Add("amountNQT", amount.Nqt.ToString());
            return await Post<TransactionCreated>("sendMoney", queryParameters);
        }

        public async Task<TransactionCreated> SetAccountInfo(CreateTransactionParameters parameters, string name,
            string description)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("name", name);
            queryParameters.Add("description", description);
            return await Post<TransactionCreated>("setAccountInfo", queryParameters);
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
