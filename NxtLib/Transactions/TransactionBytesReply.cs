namespace NxtLib.TransactionOperations
{
    public class TransactionBytesReply : BaseReply
    {
        public int Confirmations { get; set; }
        public string TransactionBytes { get; set; }
        public string UnsignedTransactionBytes { get; set; }
    }
}