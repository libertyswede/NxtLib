using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Transactions
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(string baseAddress = Constants.DefaultNxtUrl)
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

        public async Task<TransactionListReply> GetBlockchainTransactions(string accountId, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null, bool? includeExpiredPrunable = null, bool? includePhasingResult = null,
            bool? executedOnly = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> { { "account", accountId } };
            AddToParametersIfHasValue(timeStamp, queryParameters);
            if (transactionType.HasValue)
            {
                queryParameters.Add("type", TransactionTypeMapper.GetMainTypeByte(transactionType.Value).ToString());
                queryParameters.Add("subtype", TransactionTypeMapper.GetSubTypeByte(transactionType.Value).ToString());
            }
            AddToParametersIfHasValue("firstIndex", firstIndex, queryParameters);
            AddToParametersIfHasValue("lastIndex", lastIndex, queryParameters);
            AddToParametersIfHasValue("numberOfConfirmations", numberOfConfirmations, queryParameters);
            AddToParametersIfHasValue("withMessage", withMessage, queryParameters);
            AddToParametersIfHasValue("phasedOnly", phasedOnly, queryParameters);
            AddToParametersIfHasValue("nonPhasedOnly", nonPhasedOnly, queryParameters);
            AddToParametersIfHasValue("includeExpiredPrunable", includeExpiredPrunable, queryParameters);
            AddToParametersIfHasValue("includePhasingResult", includePhasingResult, queryParameters);
            AddToParametersIfHasValue("executedOnly", executedOnly, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TransactionListReply>("getBlockchainTransactions", queryParameters);
        }

        public async Task<ExpectedTransactionsReply> GetExpectedTransactions(IEnumerable<string> accountIds = null)
        {
            List<string> accountsList;
            var queryParameters = new Dictionary<string, List<string>>();

            if (accountIds != null && (accountsList = accountIds.ToList()).Any())
            {
                queryParameters.Add("account", accountsList);
            }

            return await Get<ExpectedTransactionsReply>("getExpectedTransactions", queryParameters);
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
