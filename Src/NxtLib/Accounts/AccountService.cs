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
            : base(baseAddress)
        {
        }

        public async Task<AccountReply> GetAccount(Account account, bool? includeLessors = null,
            bool? includeAssets = null, bool? includeCurrencies = null, bool? includeEffectiveBalance = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeLessors, includeLessors);
            queryParameters.AddIfHasValue(Parameters.IncludeAssets, includeAssets);
            queryParameters.AddIfHasValue(Parameters.IncludeCurrencies, includeCurrencies);
            queryParameters.AddIfHasValue(Parameters.IncludeEffectiveBalance, includeEffectiveBalance);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountReply>("getAccount", queryParameters);
        }

        public async Task<AccountBlockCountReply> GetAccountBlockCount(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountBlockCountReply>("getAccountBlockCount", queryParameters);
        }

        public async Task<AccountBlockIdsReply> GetAccountBlockIds(Account account, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Timestamp, timeStamp);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
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
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.EventType, eventType);
            queryParameters.AddIfHasValue(Parameters.Event, @event);
            queryParameters.AddIfHasValue(Parameters.HoldingType, holdingType);
            queryParameters.AddIfHasValue(Parameters.Holding, holding);
            queryParameters.AddIfHasValue(Parameters.IncludeTransactions, includeTransactions);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetAccountLedgerReply>("getAccountLedger", queryParameters);
        }

        public async Task<GetAccountLedgerEntryReply> GetAccountLedgerEntry(int ledgerId,
            bool? includeTransaction = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.LedgerId, ledgerId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeTransaction, includeTransaction);
            return await Get<GetAccountLedgerEntryReply>("getAccountLedgerEntry", queryParameters);
        }

        public async Task<AccountLessorsReply> GetAccountLessors(Account account, int? height = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.Height, height);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountLessorsReply>("getAccountLessors", queryParameters);
        }

        public async Task<AccountPropertiesReply> GetAccountProperties(Account recipient, Account setter, string property = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Recipient, recipient);
            queryParameters.AddIfHasValue(Parameters.Setter, setter);
            queryParameters.AddIfHasValue(Parameters.Property, property);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountPropertiesReply>("getAccountProperties", queryParameters);
        }

        public async Task<AccountPublicKeyReply> GetAccountPublicKey(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<AccountPublicKeyReply>("getAccountPublicKey", queryParameters);
        }

        public async Task<BalanceReply> GetBalance(Account account, bool? includeEffectiveBalance = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeEffectiveBalance, includeEffectiveBalance);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<BalanceReply>("getBalance", queryParameters);
        }

        public async Task<GuaranteedBalanceReply> GetGuaranteedBalance(Account account,
            int? numberOfConfirmations = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.NumberOfConfirmations, numberOfConfirmations);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GuaranteedBalanceReply>("getGuaranteedBalance", queryParameters);
        }

        public Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Query, query}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return Get<SearchAccountsReply>("searchAccounts", queryParameters);
        }

        public async Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, Account recipient,
            Amount amount)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add(Parameters.Recipient, recipient.AccountId.ToString());
            queryParameters.Add(Parameters.AmountNqt, amount.Nqt.ToString());
            return await Post<TransactionCreatedReply>("sendMoney", queryParameters);
        }

        public async Task<TransactionCreatedReply> SetAccountInfo(CreateTransactionParameters parameters, string name,
            string description)
        {
            var queryParameters = new Dictionary<string, string>();
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add(Parameters.Name, name);
            queryParameters.Add(Parameters.Description, description);
            return await Post<TransactionCreatedReply>("setAccountInfo", queryParameters);
        }

        public async Task<TransactionCreatedReply> SetAccountProperty(CreateTransactionParameters parameters, string property, string value = null, Account recipient = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Property, property}};
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue(Parameters.Recipient, recipient);
            queryParameters.AddIfHasValue(Parameters.Value, value);
            return await Post<TransactionCreatedReply>("setAccountProperty", queryParameters);
        }

        private static Dictionary<string, string> BuildQueryParametersForGetAccountBlocks(Account account,
            bool includeTransactions, DateTime? timeStamp, int? firstIndex,
            int? lastIndex, ulong? requireBlock, ulong? requireLastBlock)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Account, account.AccountId.ToString()},
                {Parameters.IncludeTransactions, includeTransactions.ToString()}
            };
            queryParameters.AddIfHasValue(Parameters.Timestamp, timeStamp);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return queryParameters;
        }
    }
}