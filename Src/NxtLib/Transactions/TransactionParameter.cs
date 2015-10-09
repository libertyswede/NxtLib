using Newtonsoft.Json.Linq;

namespace NxtLib.Transactions
{
    public class TransactionParameter
    {
        public string PrunableAttachmentJson { get; set; }
        public string TransactionJson { get; private set; }
        public BinaryHexString TransactionBytes { get; private set; }

        public TransactionParameter(BinaryHexString transactionBytes)
        {
            TransactionBytes = transactionBytes;
        }

        public TransactionParameter(string transactionJson)
        {
            TransactionJson = transactionJson;
        }
    }
}