using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.TaggedData
{
    public class TaggedDataService : BaseService, ITaggedDataService
    {
        public TaggedDataService(string baseUrl = Constants.DefaultNxtUrl) 
            : base(new DateTimeConverter(), baseUrl)
        {
        }

        public TaggedDataService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<TransactionCreatedReply> ExtendTaggedData(ulong transactionId, CreateTransactionParameters parameters,
            string name, string data, string description = null, string tags = null, string channel = null, 
            string type = null, bool? isText = null, string filename = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, description, tags, channel, type, isText, filename);
            queryParameters.Add("transaction", transactionId.ToString());
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("extendTaggedData", queryParameters);
        }

        public async Task<AccountTaggedDataReply> GetAccountTaggedData(string account, int? firstIndex = null, int? lastIndex = null, bool? includeData = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
            return await Get<AccountTaggedDataReply>("getAccountTaggedData", queryParameters);
        }

        public async Task<AllTaggedDataReply> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null, bool? includeData = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
            return await Get<AllTaggedDataReply>("getAllTaggedData", queryParameters);
        }

        public async Task<object> GetChannelTaggedData()
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetDataTagCount()
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetDataTags(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);

            throw new NotImplementedException();
        }

        public async Task<object> GetDataTagsLike(string tagPrefix, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>{{"tagPrefix", tagPrefix}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);

            throw new NotImplementedException();
        }

        public async Task<object> GetTaggedData(ulong transactionId)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};

            throw new NotImplementedException();
        }

        public async Task<object> SearchTaggedData(string query = null, string tag = null, string account = null, string channel = null, 
            int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("query", query, queryParameters);
            AddToParametersIfHasValue("tag", tag, queryParameters);
            AddToParametersIfHasValue("account", account, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);

            throw new NotImplementedException();
        }

        public async Task<TransactionCreatedReply> UploadTaggedData(string name, string data, CreateTransactionParameters parameters,
            string description = null, string tags = null, string channel = null, string type = null, 
            bool? isText = null, string filename = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, description, tags, channel, type, isText, filename);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("uploadTaggedData", queryParameters);
        }

        public async Task<VerifyTaggedDataReply> VerifyTaggedData(ulong transactionId, string name, string data, string description = null,
            string tags = null, string channel = null, string type = null, bool? isText = null, string filename = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, description, tags, channel, type, isText, filename);
            queryParameters.Add("transaction", transactionId.ToString());
            return await Post<VerifyTaggedDataReply>("verifyTaggedData", queryParameters);
        }

        private static Dictionary<string, string> GetQueryParametersForTaggedData(string name, string data, string description, string tags,
            string channel, string type, bool? isText, string filename)
        {
            var queryParameters = new Dictionary<string, string> { { "name", name }, { "data", data } };
            AddToParametersIfHasValue("description", description, queryParameters);
            AddToParametersIfHasValue("tags", tags, queryParameters);
            AddToParametersIfHasValue("type", type, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            AddToParametersIfHasValue("isText", isText, queryParameters);
            AddToParametersIfHasValue("filename", filename, queryParameters);
            return queryParameters;
        }
    }
}
