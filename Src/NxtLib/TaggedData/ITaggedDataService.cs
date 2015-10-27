using System.Threading.Tasks;

namespace NxtLib.TaggedData
{
    public interface ITaggedDataService
    {
        Task<DownloadTaggedDataReply> DownloadTaggedData(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> ExtendTaggedData(ulong transactionId, CreateTransactionParameters parameters,
            string name, string data, string file = null, string description = null, string tags = null,
            string channel = null, string type = null, bool? isText = null, string filename = null);

        Task<TaggedDataListReply> GetAccountTaggedData(Account account, int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<AllTaggedDataReply> GetAllTaggedData(int? firstIndex = null, int? lastIndex = null,
            bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TaggedDataListReply> GetChannelTaggedData(string channel, Account account = null, int? firstIndex = null,
            int? lastIndex = null, bool? includeData = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<DataTagCountReply> GetDataTagCount(ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<DataTagsReply> GetDataTags(int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<DataTagsReply> GetDataTagsLike(string tagPrefix, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TaggedDataReply> GetTaggedData(ulong transactionId, bool? includeData = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TaggedDataExtendTransactionsReply> GetTaggedDataExtendTransactions(ulong transactionId,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TaggedDataListReply> SearchTaggedData(string query = null, string tag = null, Account account = null,
            string channel = null, int? firstIndex = null, int? lastIndex = null, bool? includeData = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<TransactionCreatedReply> UploadTaggedData(string name, string data, CreateTransactionParameters parameters,
            string file = null, string description = null, string tags = null, string channel = null, string type = null,
            bool? isText = null, string filename = null);

        Task<VerifyTaggedDataReply> VerifyTaggedData(ulong transactionId, string name, string data, string file = null,
            string description = null, string tags = null, string channel = null, string type = null,
            bool? isText = null, string filename = null, ulong? requireBlock = null, ulong? requireLastBlock = null);
    }
}