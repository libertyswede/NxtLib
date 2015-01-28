using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

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

    public class AliasService : BaseService, IAliasService
    {
        public AliasService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public AliasService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreated> BuyAlias(AliasLocator query, Amount amount, CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("amountNQT", amount.Nqt.ToString());
            return await Post<TransactionCreated>("buyAlias", queryParameters);
        }

        public async Task<TransactionCreated> DeleteAlias(AliasLocator query, CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("deleteAlias", queryParameters);
        }

        public async Task<Alias> GetAlias(AliasLocator query)
        {
            var queryParameters = query.QueryParameters;
            return await Get<Alias>("getAlias", queryParameters);
        }

        public async Task<AliasCount> GetAliasCount(string account)
        {
            var queryParameters = new Dictionary<string, string> { { "account", account } };
            return await Get<AliasCount>("getAliasCount", queryParameters);
        }

        public async Task<Aliases> GetAliases(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<Aliases>("getAliases", queryParameters);
        }

        public async Task<TransactionCreated> SellAlias(AliasLocator query, Amount price, CreateTransactionParameters parameters, string recipient = null)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("priceNQT", price.Nqt.ToString());
            queryParameters.AddIfHasValue("recipient", recipient);
            return await Post<TransactionCreated>("sellAlias", queryParameters);
        }

        public async Task<TransactionCreated> SetAlias(string aliasName, string aliasUri,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> { { "aliasName", aliasName }, { "aliasURI", aliasUri } };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreated>("setAlias", queryParameters);
        }
    }
}
