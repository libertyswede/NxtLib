using System;
using System.Threading.Tasks;

namespace NxtLib.Accounts
{
    public interface IAccountService
    {
        Task<AccountReply> GetAccount(string accountId, bool? includeLessors = null, bool? includeAssets = null,
            bool? includeCurrencies = null);
        Task<AccountBlockCount> GetAccountBlockCount(string accountId);
        Task<AccountBlockIds> GetAccountBlockIds(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountBlocks<ulong>> GetAccountBlocks(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountBlocks<Transaction>> GetAccountBlocksIncludeTransactions(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AccountId> GetAccountId(AccountIdLocator locator);
        Task<AccountLessors> GetAccountLessors(string accountId, int? height = null);
        Task<AccountPublicKey> GetAccountPublicKey(string accountId);
        Task<AccountTransactionIds> GetAccountTransactionIds(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null);
        Task<AccountTransactions> GetAccountTransactions(string accountId, DateTime? timeStamp = null,
            byte? type = null, byte? subtype = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null);
        Task<AccountBalance> GetBalance(string accountId);
        Task<AccountGuaranteedBalance> GetGuaranteedBalance(string accountId, int? numberOfConfirmations = null);
        Task<UnconfirmedAccountTransactionIds> GetUnconfirmedTransactionIds(string accountId = null);
        Task<UnconfirmedAccountTransactions> GetUnconfirmedTransactions(string accountId = null);
        Task<TransactionCreated> SendMoney(CreateTransactionParameters parameters, string recipient, Amount amount);
        Task<TransactionCreated> SetAccountInfo(CreateTransactionParameters parameters, string name, string description);
    }
}