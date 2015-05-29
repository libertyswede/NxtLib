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
        Task<AccountLessorsReply> GetAccountLessors(string accountId, int? height = null);
        Task<AccountPublicKeyReply> GetAccountPublicKey(string accountId);
        
        [Obsolete("This API is deprecated and will be removed in 1.6. It does not include the phased transactions that an account may have. To retrieve both phased and non-phased transactions, the new getBlockchainTransactions API must be used. Do not simply switch from getAccountTransactions to getBlockchainTransactions without a detailed understanding of how phased transactions work, and without being prepared to analyze them correctly.")]
        Task<AccountTransactionIdsReply> GetAccountTransactionIds(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phased = null);

        [Obsolete("This API is deprecated and will be removed in 1.6. It does not include the phased transactions that an account may have. To retrieve both phased and non-phased transactions, the new getBlockchainTransactions API must be used. Do not simply switch from getAccountTransactions to getBlockchainTransactions without a detailed understanding of how phased transactions work, and without being prepared to analyze them correctly.")]
        Task<TransactionListReply> GetAccountTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phased = null);
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