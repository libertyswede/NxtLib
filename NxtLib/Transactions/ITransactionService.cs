using System.Threading.Tasks;

namespace NxtLib.Transactions
{
    public interface ITransactionService
    {
        Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameter parameter);

        Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString unsignedTransactionBytes,
            BinaryHexString signatureHash);

        Task<TransactionReply> GetTransaction(GetTransactionLocator locator);
        Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId);
        Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter);

        Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, string secretPhrase,
            bool? validate = null);
    }
}