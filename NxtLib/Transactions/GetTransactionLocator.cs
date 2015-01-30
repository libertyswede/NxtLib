namespace NxtLib.Transactions
{
    public class GetTransactionLocator : LocatorBase
    {
        public GetTransactionLocator(ulong transactionId) 
            : base("transaction", transactionId.ToString())
        {
        }

        public GetTransactionLocator(BinaryHexString fullHash)
            : base("fullHash", fullHash.ToHexString())
        {
        }
    }
}