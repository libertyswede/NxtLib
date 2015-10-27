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
            : base(baseUrl)
        {
        }

        public async Task<DownloadTaggedDataReply> DownloadTaggedData(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
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
            queryParameters.Add(Parameters.Transaction, transactionId.ToString());
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("extendTaggedData", queryParameters);
        }

        public async Task<TaggedDataListReply> GetAccountTaggedData(Account account, int? firstIndex = null,
            int? lastIndex = null, bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            return await Get<TaggedDataListReply>("getAccountTaggedData", queryParameters);
        }

        public async Task<AllTaggedDataReply> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            return await Get<AllTaggedDataReply>("getAllTaggedData", queryParameters);
        }

        public async Task<TaggedDataListReply> GetChannelTaggedData(string channel, Account account = null,
            int? firstIndex = null, int? lastIndex = null, bool? includeData = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(channel), channel}};
            AddToParametersIfHasValue(firstIndex, lastIndex, includeData, queryParameters);
            queryParameters.AddIfHasValue(nameof(includeData), includeData);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TaggedDataListReply>("getChannelTaggedData", queryParameters);
        }

        public async Task<DataTagCountReply> GetDataTagCount(ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<DataTagCountReply>("getDataTagCount", queryParameters);
        }

        public async Task<DataTagsReply> GetDataTags(int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<DataTagsReply>("getDataTags", queryParameters);
        }

        public async Task<DataTagsReply> GetDataTagsLike(string tagPrefix, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.TagPrefix, tagPrefix}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<DataTagsReply>("getDataTagsLike", queryParameters);
        }

        public async Task<TaggedDataReply> GetTaggedData(ulong transactionId, bool? includeData = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(includeData), includeData);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TaggedDataReply>("getTaggedData", queryParameters);
        }

        public async Task<TaggedDataExtendTransactionsReply> GetTaggedDataExtendTransactions(ulong transactionId,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TaggedDataExtendTransactionsReply>("getTaggedDataExtendTransactions", queryParameters);
        }

        public async Task<TaggedDataListReply> SearchTaggedData(string query = null, string tag = null,
            Account account = null, string channel = null, int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Query, query);
            queryParameters.AddIfHasValue(Parameters.Tag, tag);
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(nameof(channel), channel);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(nameof(includeData), includeData);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
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
            queryParameters.Add(Parameters.Transaction, transactionId.ToString());
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Post<VerifyTaggedDataReply>("verifyTaggedData", queryParameters);
        }

        private static void AddToParametersIfHasValue(int? firstIndex, int? lastIndex, bool? includeData,
            Dictionary<string, string> queryParameters)
        {
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(nameof(includeData), includeData);
        }

        private static Dictionary<string, string> GetQueryParametersForTaggedData(string name, string data, string file,
            string description, string tags, string channel, string type, bool? isText, string filename)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Name, name}};
            queryParameters.AddIfHasValue(nameof(data), data);
            queryParameters.AddIfHasValue(Parameters.File, file);
            queryParameters.AddIfHasValue(Parameters.Description, description);
            queryParameters.AddIfHasValue(Parameters.Tags, tags);
            queryParameters.AddIfHasValue(nameof(type), type);
            queryParameters.AddIfHasValue(nameof(channel), channel);
            queryParameters.AddIfHasValue(nameof(isText), isText);
            queryParameters.AddIfHasValue(nameof(filename), filename);
            return queryParameters;
        }
    }
}