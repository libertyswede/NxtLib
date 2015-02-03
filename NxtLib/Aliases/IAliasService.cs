using System;
using System.Threading.Tasks;

namespace NxtLib.Aliases
{
    public interface IAliasService
    {
        Task<TransactionCreated> BuyAlias(AliasLocator query, Amount amount, CreateTransactionParameters parameters);
        Task<TransactionCreated> DeleteAlias(AliasLocator query, CreateTransactionParameters parameters);
        Task<Alias> GetAlias(AliasLocator query);
        Task<AliasCount> GetAliasCount(string accoun);
        Task<Aliases> GetAliases(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<TransactionCreated> SellAlias(AliasLocator query, Amount price, CreateTransactionParameters parameters, string recipient = null);
        Task<TransactionCreated> SetAlias(string aliasName, string aliasUri, CreateTransactionParameters parameters);
    }
}