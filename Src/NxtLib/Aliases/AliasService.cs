using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Aliases
{
    public class AliasService : BaseService, IAliasService
    {
        public AliasService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AliasService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> BuyAlias(AliasLocator query, Amount amount,
            CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("amountNQT", amount.Nqt.ToString());
            return await Post<TransactionCreatedReply>("buyAlias", queryParameters);
        }

        public async Task<TransactionCreatedReply> DeleteAlias(AliasLocator query,
            CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("deleteAlias", queryParameters);
        }

        public async Task<AliasReply> GetAlias(AliasLocator query, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<AliasReply>("getAlias", queryParameters);
        }

        public async Task<AliasCountReply> GetAliasCount(Account account, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<AliasCountReply>("getAliasCount", queryParameters);
        }

        public async Task<AliasesReply> GetAliases(Account account, DateTime? timeStamp = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(timeStamp);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<AliasesReply>("getAliases", queryParameters);
        }

        public async Task<AliasesReply> GetAliasesLike(string prefix, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(prefix), prefix}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<AliasesReply>("getAliasesLike", queryParameters);
        }

        public async Task<TransactionCreatedReply> SellAlias(AliasLocator query, Amount price,
            CreateTransactionParameters parameters, Account recipient = null)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("priceNQT", price.Nqt.ToString());
            queryParameters.AddIfHasValue(nameof(recipient), recipient);
            return await Post<TransactionCreatedReply>("sellAlias", queryParameters);
        }

        public async Task<TransactionCreatedReply> SetAlias(string aliasName, string aliasUri,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(aliasName), aliasName}, {"aliasURI", aliasUri}};
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("setAlias", queryParameters);
        }
    }
}