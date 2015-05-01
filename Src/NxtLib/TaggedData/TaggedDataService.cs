using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.TaggedData
{
    public class TaggedDataService : BaseService, ITaggedDataService
    {
        public TaggedDataService(string baseUrl = DefaultBaseUrl) 
            : base(new DateTimeConverter(), baseUrl)
        {
        }

        public TaggedDataService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<object> ExtendTaggedData(ulong transactionId, CreateTransactionParameters parameters,
            string name, string data, string description = null, string tags = null, string type = null,
            bool? isText = null, string filename = null, string channel = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("name", name, queryParameters);
            AddToParametersIfHasValue("data", data, queryParameters);
            AddToParametersIfHasValue("description", description, queryParameters);
            AddToParametersIfHasValue("tags", tags, queryParameters);
            AddToParametersIfHasValue("type", type, queryParameters);
            AddToParametersIfHasValue("isText", isText, queryParameters);
            AddToParametersIfHasValue("filename", filename, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);

            throw new NotImplementedException();
        }

        public async Task<object> GetAccountTaggedData(string account, int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);

            throw new NotImplementedException();
        }

        public async Task<object> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);

            throw new NotImplementedException();
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
            var queryParameters = new Dictionary<string, string> {{"name", name}, {"data", data}};
            AddToParametersIfHasValue("description", description, queryParameters);
            AddToParametersIfHasValue("tags", tags, queryParameters);
            AddToParametersIfHasValue("type", type, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            AddToParametersIfHasValue("isText", isText, queryParameters);
            AddToParametersIfHasValue("filename", filename, queryParameters);
            parameters.AppendToQueryParameters(queryParameters);

            return await Post<TransactionCreatedReply>("uploadTaggedData", queryParameters);
        }

        public async Task<object> VerifyTaggedData(ulong transactionId, string name, string data, string description = null,
            string tags = null, string channel = null, string type = null, bool? isText = null, string filename = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"transaction", transactionId.ToString()},
                {"name", name},
                {"data", data}
            };
            AddToParametersIfHasValue("description", description, queryParameters);
            AddToParametersIfHasValue("tags", tags, queryParameters);
            AddToParametersIfHasValue("type", type, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            AddToParametersIfHasValue("isText", isText, queryParameters);
            AddToParametersIfHasValue("filename", filename, queryParameters);

            throw new NotImplementedException();
        }
    }
}
