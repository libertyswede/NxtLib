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

        public async Task<CalculateFullHashReply> CalculateFullHash(BinaryHexString signatureHash, 
            BinaryHexString unsignedTransactionBytes = null, string unsignedTransactionJson = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(signatureHash), signatureHash.ToHexString()}};
            if (unsignedTransactionBytes != null)
            {
                queryParameters.Add(nameof(unsignedTransactionBytes), unsignedTransactionBytes.ToHexString());
            }
            queryParameters.AddIfHasValue(nameof(unsignedTransactionJson), unsignedTransactionJson);
            return await Get<CalculateFullHashReply>("calculateFullHash", queryParameters);
        }

        public async Task<TransactionListReply> GetBlockchainTransactions(Account account, DateTime? timeStamp = null,
            TransactionSubType? transactionType = null, int? firstIndex = null, int? lastIndex = null,
            int? numberOfConfirmations = null, bool? withMessage = null, bool? phasedOnly = null,
            bool? nonPhasedOnly = null, bool? includeExpiredPrunable = null, bool? includePhasingResult = null,
            bool? executedOnly = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(account), account.AccountId.ToString()}};
            if (transactionType.HasValue)
            {
                queryParameters.Add("type", TransactionTypeMapper.GetMainTypeByte(transactionType.Value).ToString());
                queryParameters.Add("subtype", TransactionTypeMapper.GetSubTypeByte(transactionType.Value).ToString());
            }
            queryParameters.AddIfHasValue(timeStamp);
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(numberOfConfirmations), numberOfConfirmations);
            queryParameters.AddIfHasValue(nameof(withMessage), withMessage);
            queryParameters.AddIfHasValue(nameof(phasedOnly), phasedOnly);
            queryParameters.AddIfHasValue(nameof(nonPhasedOnly), nonPhasedOnly);
            queryParameters.AddIfHasValue(nameof(includeExpiredPrunable), includeExpiredPrunable);
            queryParameters.AddIfHasValue(nameof(includePhasingResult), includePhasingResult);
            queryParameters.AddIfHasValue(nameof(executedOnly), executedOnly);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionListReply>("getBlockchainTransactions", queryParameters);
        }

        public async Task<ExpectedTransactionsReply> GetExpectedTransactions(IEnumerable<Account> accounts = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            List<Account> accountsList;
            var queryParameters = new Dictionary<string, List<string>>();

            if (accounts != null && (accountsList = accounts.ToList()).Any())
            {
                queryParameters.Add("account", accountsList.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ExpectedTransactionsReply>("getExpectedTransactions", queryParameters);
        }

        public async Task<TransactionReply> GetTransaction(GetTransactionLocator locator,
            bool? includePhasingResult = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = locator.QueryParameters;
            queryParameters.AddIfHasValue(nameof(includePhasingResult), includePhasingResult);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionReply>("getTransaction", queryParameters);
        }

        public async Task<TransactionBytesReply> GetTransactionBytes(ulong transactionId, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"transaction", transactionId.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<TransactionBytesReply>("getTransactionBytes", queryParameters);
        }

        public async Task<UnconfirmedTransactionIdsResply> GetUnconfirmedTransactionIds(
            IEnumerable<Account> accounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>();
            if (accounts != null)
            {
                queryParameters.Add("account", accounts.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<UnconfirmedTransactionIdsResply>("getUnconfirmedTransactionIds", queryParameters);
        }

        public async Task<UnconfirmedTransactionsReply> GetUnconfirmedTransactions(
            IEnumerable<Account> accounts = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, List<string>>();
            if (accounts != null)
            {
                queryParameters.Add("account", accounts.Select(a => a.AccountId.ToString()).ToList());
            }
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<UnconfirmedTransactionsReply>("getUnconfirmedTransactions", queryParameters);
        }

        public async Task<ParseTransactionReply> ParseTransaction(TransactionParameter parameter, 
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = CreateQueryParameters(parameter);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<ParseTransactionReply>("parseTransaction", queryParameters);
        }

        public async Task<SignTransactionReply> SignTransaction(TransactionParameter parameter, 
            string secretPhrase, bool? validate = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = CreateQueryParameters(parameter, true);
            queryParameters.Add(nameof(secretPhrase), secretPhrase);
            queryParameters.AddIfHasValue(nameof(validate), validate);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<SignTransactionReply>("signTransaction", queryParameters);
        }

        private static Dictionary<string, string> CreateQueryParameters(TransactionParameter parameter,
            bool unsigned = false)
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