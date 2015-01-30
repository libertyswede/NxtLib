namespace NxtLib.Transactions
{
    public class BroadcastTransactionParameter
    {
        public string TransactionJson { get; private set; }
        public BinaryHexString TransactionBytes { get; private set; }

        public BroadcastTransactionParameter(BinaryHexString transactionBytes)
        {
            TransactionBytes = transactionBytes;
        }

        public BroadcastTransactionParameter(string transactionJson)
        {
            TransactionJson = transactionJson;
        }
    }
}