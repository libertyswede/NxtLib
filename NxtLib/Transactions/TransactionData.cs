namespace NxtLib.Transactions
{
    public class TransactionData
    {
        public string TransactionJson { get; private set; }
        public BinaryHexString TransactionBytes { get; private set; }

        public TransactionData(BinaryHexString transactionBytes)
        {
            TransactionBytes = transactionBytes;
        }

        public TransactionData(string transactionJson)
        {
            TransactionJson = transactionJson;
        }
    }
}