using System.Threading.Tasks;

namespace NxtLib.TaggedData
{
    public interface ITaggedDataService
    {
        Task<TransactionCreatedReply> ExtendTaggedData(ulong transactionId, CreateTransactionParameters parameters,
            string name, string data, string description = null, string tags = null, string channel = null, 
            string type = null, bool? isText = null, string filename = null);

        Task<TaggedDataListReply> GetAccountTaggedData(string account, int? firstIndex = null, int? lastIndex = null, bool? includeData = null);

        Task<AllTaggedDataReply> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null, bool? includeData = null);

        Task<TaggedDataListReply> GetChannelTaggedData(string channel, string account = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeData = null);

        Task<DataTagCountReply> GetDataTagCount();

        Task<DataTagsReply> GetDataTags(int? firstIndex = null, int? lastIndex = null);

        Task<DataTagsReply> GetDataTagsLike(string tagPrefix, int? firstIndex = null, int? lastIndex = null);

        Task<TaggedDataReply> GetTaggedData(ulong transactionId, bool? includeData = null);

        Task<TaggedDataListReply> SearchTaggedData(string query = null, string tag = null, string account = null, string channel = null,
            int? firstIndex = null, int? lastIndex = null, bool? includeData = null);

        Task<TransactionCreatedReply> UploadTaggedData(string name, string data, CreateTransactionParameters parameters,
            string description = null, string tags = null, string channel = null, string type = null, 
            bool? isText = null, string filename = null);

        Task<VerifyTaggedDataReply> VerifyTaggedData(ulong transactionId, string name, string data, string description = null,
            string tags = null, string channel = null, string type = null, bool? isText = null, string filename = null);
    }
}