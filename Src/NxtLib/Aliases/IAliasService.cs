using System;
using System.Threading.Tasks;

namespace NxtLib.Aliases
{
    public interface IAliasService
    {
        Task<TransactionCreatedReply> BuyAlias(AliasLocator query, Amount amount, CreateTransactionParameters parameters);
        Task<TransactionCreatedReply> DeleteAlias(AliasLocator query, CreateTransactionParameters parameters);
        Task<AliasReply> GetAlias(AliasLocator query);
        Task<AliasCountReply> GetAliasCount(string accoun);
        Task<AliasesReply> GetAliases(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null);
        Task<AliasesReply> GetAliasesLike(string prefix, int? firstIndex = null, int? lastIndex = null);
        Task<TransactionCreatedReply> SellAlias(AliasLocator query, Amount price, CreateTransactionParameters parameters, string recipient = null);
        Task<TransactionCreatedReply> SetAlias(string aliasName, string aliasUri, CreateTransactionParameters parameters);
    }
}