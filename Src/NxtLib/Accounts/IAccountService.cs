using System;
using System.Threading.Tasks;

namespace NxtLib.Accounts
{
    public interface IAccountService
    {
        Task<AccountReply> GetAccount(string accountId, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null, bool? includeEffectiveBalance = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountBlockCountReply> GetAccountBlockCount(string accountId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountBlockIdsReply> GetAccountBlockIds(string accountId, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountBlocksReply<ulong>> GetAccountBlocks(string accountId, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountBlocksReply<Transaction>> GetAccountBlocksIncludeTransactions(string accountId,
            DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountIdReply> GetAccountId(AccountIdLocator locator);

        Task<GetAccountLedgerReply> GetAccountLedger(string accountId, int? firstIndex = null,
            int? lastIndex = null, string eventType = null, ulong? @event = null, string holdingType = null,
            ulong? holding = null, bool? includeTransactions = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetAccountLedgerEntryReply> GetAccountLedgerEntry(int ledgerId,
            bool? includeTransaction = null);

        Task<AccountLessorsReply> GetAccountLessors(string accountId, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountPublicKeyReply> GetAccountPublicKey(string accountId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<BalanceReply> GetBalance(string accountId, bool? includeEffectiveBalance = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionListReply> GetBlockchainTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null, bool? includeExpiredPrunable = null, bool? includePhasingResult = null,
            bool? executedOnly = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GuaranteedBalanceReply> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, string recipient, Amount amount);

        Task<TransactionCreatedReply> SetAccountInfo(CreateTransactionParameters parameters, string name,
            string description);
    }
}