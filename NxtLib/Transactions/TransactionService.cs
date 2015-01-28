using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
    public interface ITransactionService
    {
        Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameters parameters);

        Task<CalculateFullHashReply> CalculateFullHash(string unsignedTransactionBytes,
            string signatureHash);

        Task<TransactionReply> GetTransaction(GetTransactionLocator locator);
        Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId);
        Task<ParseTransactionReply> ParseTransaction(TransactionParameters parameters);

        Task<SignTransactionReply> SignTransaction(TransactionParameters parameters, string secretPhrase,
            bool? validate = null);
    }

    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public TransactionService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameters parameters)
        {
            var queryParameters = CreateQueryParameters(parameters);
            return await Post<BroadcastTransactionReply>("broadcastTransaction", queryParameters);
        }

        public async Task<CalculateFullHashReply> CalculateFullHash(string unsignedTransactionBytes,
            string signatureHash)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"unsignedTransactionBytes", unsignedTransactionBytes},
                {"signatureHash", signatureHash}
            };
            return await Get<CalculateFullHashReply>("calculateFullHash", queryParameters);
        }

        public async Task<TransactionReply> GetTransaction(GetTransactionLocator locator)
        {
            return await Get<TransactionReply>("getTransaction", locator.QueryParameters);
        }

        public async Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            return await Get<TransactionBytesReply>("getTransactionBytes", queryParameters);
        }

        public async Task<ParseTransactionReply> ParseTransaction(TransactionParameters parameters)
        {
            var queryParameters = CreateQueryParameters(parameters);
            return await Get<ParseTransactionReply>("parseTransaction", queryParameters);
        }

        public async Task<SignTransactionReply> SignTransaction(TransactionParameters parameters, string secretPhrase,
            bool? validate = null)
        {
            var queryParameters = CreateQueryParameters(parameters);
            queryParameters.Add("secretPhrase", secretPhrase);
            AddToParametersIfHasValue("validate", validate, queryParameters);
            return await Get<SignTransactionReply>("signTransaction", queryParameters);
        }

        private static Dictionary<string, string> CreateQueryParameters(TransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(parameters.TransactionBytes))
            {
                queryParameters.Add("transactionBytes", parameters.TransactionBytes);
            }
            if (!string.IsNullOrEmpty(parameters.TransactionJson))
            {
                queryParameters.Add("transactionJSON", parameters.TransactionJson);
            }
            return queryParameters;
        }
    }
}
