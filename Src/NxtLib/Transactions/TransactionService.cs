using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.Transactions
{
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

        public async Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameter parameter)
        {
            var queryParameters = CreateQueryParameters(parameter);
            return await Post<BroadcastTransactionReply>("broadcastTransaction", queryParameters);
        }

        public async Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString unsignedTransactionBytes,
            BinaryHexString signatureHash)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"unsignedTransactionBytes", unsignedTransactionBytes.ToHexString()},
                {"signatureHash", signatureHash.ToHexString()}
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

        public async Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedTransactionIdsResply>("getUnconfirmedTransactionIds", queryParameters);
        }

        public async Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(string accountId = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("account", accountId, queryParameters);
            return await Get<UnconfirmedTransactionsReply>("getUnconfirmedTransactions", queryParameters);
        }

        public async Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter)
        {
            var queryParameters = CreateQueryParameters(parameter);
            return await Get<ParseTransactionReply>("parseTransaction", queryParameters);
        }

        public async Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, string secretPhrase,
            bool? validate = null)
        {
            var queryParameters = CreateQueryParameters(parameter, true);
            queryParameters.Add("secretPhrase", secretPhrase);
            AddToParametersIfHasValue("validate", validate, queryParameters);
            return await Get<SignTransactionReply>("signTransaction", queryParameters);
        }

        private static Dictionary<string, string> CreateQueryParameters(TransactionParameter parameter, bool unsigned = false)
        {
            var queryParameters = new Dictionary<string, string>();
            if (parameter.TransactionBytes != null)
            {
                queryParameters.Add(!unsigned ? "transactionBytes" : "unsignedTransactionBytes",
                    parameter.TransactionBytes.ToHexString());
            }
            if (parameter.TransactionJson != null)
            {
                queryParameters.Add(!unsigned ? "transactionJSON" : "unsignedTransactionJSON",
                    parameter.TransactionJson);
            }
            if (!string.IsNullOrEmpty(parameter.PrunableAttachmentJson))
            {
                queryParameters.Add("prunableAttachmentJSON", parameter.PrunableAttachmentJson);
            }
            return queryParameters;
        }
    }
}
