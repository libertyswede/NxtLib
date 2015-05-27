using System.Threading.Tasks;

namespace NxtLib.TaggedData
{
    public interface ITaggedDataService
    {
        Task<TransactionCreatedReply> UploadTaggedData(string name, string data, CreateTransactionParameters parameters,
            string description = null, string tags = null, string channel = null, string type = null, 
            bool? isText = null, string filename = null);
    }
}