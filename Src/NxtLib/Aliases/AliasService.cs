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

        public async Task<TransactionCreatedReply> BuyAlias(AliasLocator query, Amount amount, CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("amountNQT", amount.Nqt.ToString());
            return await Post<TransactionCreatedReply>("buyAlias", queryParameters);
        }

        public async Task<TransactionCreatedReply> DeleteAlias(AliasLocator query, CreateTransactionParameters parameters)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("deleteAlias", queryParameters);
        }

        public async Task<AliasReply> GetAlias(AliasLocator query)
        {
            var queryParameters = query.QueryParameters;
            return await Get<AliasReply>("getAlias", queryParameters);
        }

        public async Task<AliasCountReply> GetAliasCount(string account)
        {
            var queryParameters = new Dictionary<string, string> { { "account", account } };
            return await Get<AliasCountReply>("getAliasCount", queryParameters);
        }

        public async Task<AliasesReply> GetAliases(string accountId, DateTime? timeStamp = null, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AliasesReply>("getAliases", queryParameters);
        }

        public async Task<AliasesReply> GetAliasesLike(string prefix, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"prefix", prefix}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            return await Get<AliasesReply>("getAliasesLike", queryParameters);
        }

        public async Task<TransactionCreatedReply> SellAlias(AliasLocator query, Amount price, CreateTransactionParameters parameters, string recipient = null)
        {
            var queryParameters = query.QueryParameters;
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.Add("priceNQT", price.Nqt.ToString());
            queryParameters.AddIfHasValue("recipient", recipient);
            return await Post<TransactionCreatedReply>("sellAlias", queryParameters);
        }

        public async Task<TransactionCreatedReply> SetAlias(string aliasName, string aliasUri,
            CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string> { { "aliasName", aliasName }, { "aliasURI", aliasUri } };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("setAlias", queryParameters);
        }
    }
}
