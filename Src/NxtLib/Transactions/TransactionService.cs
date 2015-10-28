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
            : base(baseAddress)
        {
        }

        public async Task<BroadcastTransactionReply> BroadcastTransaction(TransactionParameter parameter)
        {
            var queryParameters = CreateQueryParameters(parameter);
            return await Post<BroadcastTransactionReply>("broadcastTransaction", queryParameters);
        }

        public async Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString signatureHash, 
            BinaryHexString unsignedTransactionBytes = null, string unsignedTransactionJson = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.SignatureHash, signatureHash.ToHexString()}};
            if (unsignedTransactionBytes != null)
            {
                queryParameters.Add(Parameters.UnsignedTransactionBytes, unsignedTransactionBytes.ToHexString());
            }
            queryParameters.AddIfHasValue(Parameters.UnsignedTransactionJson, unsignedTransactionJson);
            return await Get<CalculateFullHashReply>("calculateFullHash", queryParameters);
        }

        public async Task<TransactionListReply> GetBlockchainTransactions(Account account, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null, bool? includeExpiredPrunable = null, bool? includePhasingResult = null,
            bool? executedOnly = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountId.ToString()}};
            if (transactionType.HasValue)
            {
                queryParameters.Add(Parameters.Type, TransactionTypeMapper.GetMainTypeByte(transactionType.Value).ToString());
                queryParameters.Add(Parameters.SubType, TransactionTypeMapper.GetSubTypeByte(transactionType.Value).ToString());
            }
            queryParameters.AddIfHasValue(Parameters.Timestamp, timeStamp);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.NumberOfConfirmations, numberOfConfirmations);
            queryParameters.AddIfHasValue(Parameters.WithMessage, withMessage);
            queryParameters.AddIfHasValue(Parameters.PhasedOnly, phasedOnly);
            queryParameters.AddIfHasValue(Parameters.NonPhasedOnly, nonPhasedOnly);
            queryParameters.AddIfHasValue(Parameters.IncludeExpiredPrunable, includeExpiredPrunable);
            queryParameters.AddIfHasValue(Parameters.IncludePhasingResult, includePhasingResult);
            queryParameters.AddIfHasValue(Parameters.ExecutedOnly, executedOnly);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionListReply>("getBlockchainTransactions", queryParameters);
        }

        public async Task<ExpectedTransactionsReply> GetExpectedTransactions(IEnumerable<Account> accounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            List<Account> accountsList;
            var queryParameters = new Dictionary<string, List<string>>();

            if (accounts != null && (accountsList = accounts.ToList()).Any())
            {
                queryParameters.Add(Parameters.Account, accountsList.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ExpectedTransactionsReply>("getExpectedTransactions", queryParameters);
        }

        public async Task<TransactionReply> GetTransaction(GetTransactionLocator locator,
            bool? includePhasingResult = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.IncludePhasingResult, includePhasingResult);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionReply>("getTransaction", queryParameters);
        }

        public async Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Transaction, transactionId.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<TransactionBytesReply>("getTransactionBytes", queryParameters);
        }

        public async Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(
            IEnumerable<Account> accounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>();
            if (accounts != null)
            {
                queryParameters.Add(Parameters.Account, accounts.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<UnconfirmedTransactionIdsResply>("getUnconfirmedTransactionIds", queryParameters);
        }

        public async Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(
            IEnumerable<Account> accounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>();
            if (accounts != null)
            {
                queryParameters.Add(Parameters.Account, accounts.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<UnconfirmedTransactionsReply>("getUnconfirmedTransactions", queryParameters);
        }

        public async Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter, 
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = CreateQueryParameters(parameter);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ParseTransactionReply>("parseTransaction", queryParameters);
        }

        public async Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, 
            string secretPhrase, bool? validate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = CreateQueryParameters(parameter, true);
            queryParameters.Add(Parameters.SecretPhrase, secretPhrase);
            queryParameters.AddIfHasValue(Parameters.Validate, validate);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<SignTransactionReply>("signTransaction", queryParameters);
        }

        private static Dictionary<string, string> CreateQueryParameters(TransactionParameter parameter,
            bool unsigned = false)
        {
            var queryParameters = new Dictionary<string, string>();
            if (parameter.TransactionBytes != null)
            {
                queryParameters.Add(!unsigned ? Parameters.TransactionBytes : Parameters.UnsignedTransactionBytes,
                    parameter.TransactionBytes.ToHexString());
            }
            if (parameter.TransactionJson != null)
            {
                queryParameters.Add(!unsigned ? Parameters.TransactionJson : Parameters.UnsignedTransactionJson,
                    parameter.TransactionJson);
            }
            if (!string.IsNullOrEmpty(parameter.PrunableAttachmentJson))
            {
                queryParameters.Add(Parameters.PrunableAttachmentJson, parameter.PrunableAttachmentJson);
            }
            return queryParameters;
        }
    }
}