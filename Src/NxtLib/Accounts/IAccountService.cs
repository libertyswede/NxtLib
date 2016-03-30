using System;
using System.Threading.Tasks;

namespace NxtLib.Accounts
{
    public interface IAccountService
    {
        Task<TransactionCreatedReply> DeleteAccountProperty(CreateTransactionParameters parameters,
            string property, Account recipient = null, Account setter = null);

        Task<AccountReply> GetAccount(Account account, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null, bool? includeEffectiveBalance = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountBlockCountReply> GetAccountBlockCount(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountBlockIdsReply> GetAccountBlockIds(Account account, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountBlocksReply<ulong>> GetAccountBlocks(Account account, DateTime? timeStamp = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountBlocksReply<Transaction>> GetAccountBlocksIncludeTransactions(Account account,
            DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountIdReply> GetAccountId(AccountIdLocator locator);

        Task<GetAccountLedgerReply> GetAccountLedger(Account account, int? firstIndex = null, int? lastIndex = null,
            string eventType = null, ulong? @event = null, string holdingType = null, ulong? holding = null,
            bool? includeTransactions = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetAccountLedgerEntryReply> GetAccountLedgerEntry(int ledgerId,
            bool? includeTransaction = null);

        Task<AccountLessorsReply> GetAccountLessors(Account account, int? height = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<AccountPropertiesReply> GetAccountProperties(Account recipient, Account setter, string property = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AccountPublicKeyReply> GetAccountPublicKey(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<BalanceReply> GetBalance(Account account, bool? includeEffectiveBalance = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GuaranteedBalanceReply> GetGuaranteedBalance(Account account, int? numberOfConfirmations = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<SearchAccountsReply> SearchAccounts(string query, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> SendMoney(CreateTransactionParameters parameters, Account recipient, Amount amount);

        Task<TransactionCreatedReply> SetAccountInfo(CreateTransactionParameters parameters, string name,
            string description);

        Task<TransactionCreatedReply> SetAccountProperty(CreateTransactionParameters parameters, string property,
            string value = null, Account recipient = null);

        Task<StartedReply> StartFundingMonitor(string property, string secretPhrase, Amount amount = null,
            Amount threshold = null, int? interval = null, HoldingType? holdingType = null, ulong? holding = null);

        Task<StoppedReply> StopFundingMonitor(HoldingType? holdingType = null, ulong? holding = null,
            string property = null, string secretPhrase = null, Account account = null, string adminPassword = null);
    }
}