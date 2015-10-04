using System;
using System.Threading.Tasks;

namespace NxtLib.Accounts
{
    public interface IAccountService
    {
        Task<AccountReply> GetAccount(string accountId, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null, bool? includeEffectiveBalance = null);
        Task<AccountBlockCountReply> GetAccountBlockCount(string accountId);
        Task<AccountBlockIdsReply> GetAccountBlockIds(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountBlocksReply<ulong>> GetAccountBlocks(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountBlocksReply<Transaction>> GetAccountBlocksIncludeTransactions(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountIdReply> GetAccountId(AccountIdLocator locator);
        Task<GetAccountLedgerReply> GetAccountLedger(string accountId, int? firstIndex = null,
            int? lastIndex = null, string eventType = null, ulong? @event = null, string holdingType = null,
            ulong? holding = null, bool? includeTransactions = null);
        Task<AccountLessorsReply> GetAccountLessors(string accountId, int? height = null);
        Task<AccountPublicKeyReply> GetAccountPublicKey(string accountId);
        Task<BalanceReply> GetBalance(string accountId, bool? includeEffectiveBalance = null);
        Task<TransactionListReply> GetBlockchainTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null);
        Task<GuaranteedBalanceReply> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null);
        Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null);
        Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, string recipient, Amount amount);
        Task<TransactionCreatedReply> SetAccountInfo(CreateTransactionParameters parameters, string name, string description);
    }
}