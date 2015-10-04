using System.Collections.Generic;
using System.Threading.Tasks;

namespace NxtLib.Transactions
{
    public interface ITransactionService
    {
        Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameter parameter);

        Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString unsignedTransactionBytes,
            BinaryHexString signatureHash);
        Task<ExpectedTransactionsReply> GetExpectedTransactions(IEnumerable<string> accountIds = null);
        Task<TransactionReply> GetTransaction(GetTransactionLocator locator);
        Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId);
        Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(string accountId = null);
        Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(string accountId = null);
        Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter);
        Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, string secretPhrase,
            bool? validate = null);

    }
}