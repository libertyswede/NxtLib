using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<DownloadTaggedDataReply> DownloadTaggedData(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            var url = BuildUrl("downloadTaggedData", queryParameters);

            using (var client = new HttpClient())
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                var reply = new DownloadTaggedDataReply();
                reply.Content = await content.ReadAsStringAsync();
                reply.FileName = content.Headers.ContentDisposition.FileName;
                return reply;
            }
        }

        public async Task<TransactionCreatedReply> ExtendTaggedData(ulong transactionId,
            CreateTransactionParameters parameters, string name, string data, string file = null,
            string description = null, string tags = null, string channel = null, string type = null,
            bool? isText = null, string filename = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, file, description, tags, channel, type, isText, filename);
            queryParameters.Add("transaction", transactionId.ToString());
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("extendTaggedData", queryParameters);
        }

        public async Task<TaggedDataListReply> GetAccountTaggedData(Account account, int? firstIndex = null,
            int? lastIndex = null, bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"account", account.AccountId.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            return await Get<TaggedDataListReply>("getAccountTaggedData", queryParameters);
        }

        public async Task<AllTaggedDataReply> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            return await Get<AllTaggedDataReply>("getAllTaggedData", queryParameters);
        }

        public async Task<TaggedDataListReply> GetChannelTaggedData(string channel, Account account = null,
            int? firstIndex = null, int? lastIndex = null, bool? includeData = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"channel", channel}};
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TaggedDataListReply>("getChannelTaggedData", queryParameters);
        }

        public async Task<DataTagCountReply> GetDataTagCount(ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<DataTagCountReply>("getDataTagCount", queryParameters);
        }

        public async Task<DataTagsReply> GetDataTags(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<DataTagsReply>("getDataTags", queryParameters);
        }

        public async Task<DataTagsReply> GetDataTagsLike(string tagPrefix, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"tagPrefix", tagPrefix}};
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<DataTagsReply>("getDataTagsLike", queryParameters);
        }

        public async Task<TaggedDataReply> GetTaggedData(ulong transactionId, bool? includeData = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TaggedDataReply>("getTaggedData", queryParameters);
        }

        public async Task<TaggedDataExtendTransactionsReply> GetTaggedDataExtendTransactions(ulong transactionId,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TaggedDataExtendTransactionsReply>("getTaggedDataExtendTransactions", queryParameters);
        }

        public async Task<TaggedDataListReply> SearchTaggedData(string query = null, string tag = null,
            Account account = null, string channel = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("query", query, queryParameters);
            AddToParametersIfHasValue("tag", tag, queryParameters);
            AddToParametersIfHasValue("account", account, queryParameters);
            AddToParametersIfHasValue("channel", channel, queryParameters);
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TaggedDataListReply>("searchTaggedData", queryParameters);
        }

        public async Task<TransactionCreatedReply> UploadTaggedData(string name, string data,
            CreateTransactionParameters parameters, string file = null, string description = null, string tags = null,
            string channel = null, string type = null, bool? isText = null, string filename = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, file, description, tags, channel, type,
                isText, filename);
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("uploadTaggedData", queryParameters);
        }

        public async Task<VerifyTaggedDataReply> VerifyTaggedData(ulong transactionId, string name, string data,
            string file = null, string description = null, string tags = null, string channel = null, string type = null,
            bool? isText = null, string filename = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = GetQueryParametersForTaggedData(name, data, file, description, tags, channel, type,
                isText, filename);
            queryParameters.Add("transaction", transactionId.ToString());
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Post<VerifyTaggedDataReply>("verifyTaggedData", queryParameters);
        }

        private static void AddToParametersIfHasValue(int? firstIndex, int? lastIndex, bool? includeData,
            Dictionary<string, string> queryParameters)
        {
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("includeData", includeData, queryParameters);
        }

        private static Dictionary<string, string> GetQueryParametersForTaggedData(string name, string data, string file,
            string description, string tags, string channel, string type, bool? isText, string filename)
        {
            var queryParameters = new Dictionary<string, string> {{"name", name}};
            AddToParametersIfHasValue("data", data, queryParameters);
            AddToParametersIfHasValue("file", file, queryParameters);
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