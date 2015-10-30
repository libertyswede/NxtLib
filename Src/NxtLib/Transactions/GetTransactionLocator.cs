using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public class GetTransactionLocator : LocatorBase
    {
        public readonly ulong? TransactionId;
        public readonly BinaryHexString FullHash;

        private GetTransactionLocator(ulong transactionId) 
            : base(Parameters.Transaction, transactionId.ToString())
        {
            TransactionId = transactionId;
        }

        private GetTransactionLocator(BinaryHexString fullHash)
            : base(Parameters.FullHash, fullHash.ToHexString())
        {
            FullHash = fullHash;
        }

        public static implicit operator GetTransactionLocator(ulong transactionId)
        {
            return new GetTransactionLocator(transactionId);
        }

        public static implicit operator GetTransactionLocator(BinaryHexString fullHash)
        {
            return new GetTransactionLocator(fullHash);
        }

        public static GetTransactionLocator ByTransactionId(ulong transactionId)
        {
            return new GetTransactionLocator(transactionId);
        }

        public static GetTransactionLocator ByFullHash(BinaryHexString fullHash)
        {
            return new GetTransactionLocator(fullHash);
        }
    }
}