using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Shuffling
{
    public class ShufflingService : BaseService, IShufflingService
    {
        public ShufflingService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
        }

        public async Task<ShufflingsReply> GetAccountShufflings(Account account, bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountRs}};
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.IncludeHoldingInfo, includeHoldingInfo);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingsReply>("getAccountShufflings", queryParameters);
        }

        public async Task<ShufflingsReply> GetAllShufflings(bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.IncludeHoldingInfo, includeHoldingInfo);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingsReply>("getAllShufflings", queryParameters);
        }

        public async Task<ShufflingsReply> GetAssignedShufflings(Account account, bool? includeHoldingInfo = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Account, account.AccountRs}};
            queryParameters.AddIfHasValue(Parameters.IncludeHoldingInfo, includeHoldingInfo);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingsReply>("getAssignedShufflings", queryParameters);
        }

        public async Task<ShufflingsReply> GetHoldingShufflings(ulong? holding = null, ShufflingStage? stage = null,
            bool? includeFinished = null, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Holding, holding);
            queryParameters.AddIfHasValue(Parameters.Stage, stage.HasValue ? (int?) stage : null);
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingsReply>("getHoldingShufflings", queryParameters);
        }

        public async Task<ShufflersReply> GetShufflers(Account account = null, BinaryHexString shufflingFullHash = null,
            SecretPhraseOrAdminPassword sercretPhraseOrAdminPassword = null)
        {
            var queryParameters = sercretPhraseOrAdminPassword != null ? sercretPhraseOrAdminPassword.QueryParameters : new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.ShufflingFullHash, shufflingFullHash);
            return await Get<ShufflersReply>("getShufflers", queryParameters);
        }

        public async Task<ShufflingReply> GetShuffling(ulong shuffling, bool? includeHoldingInfo = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Shuffling, shuffling.ToString()}};
            queryParameters.AddIfHasValue(Parameters.IncludeHoldingInfo, includeHoldingInfo);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingReply>("getShuffling", queryParameters);
        }

        public async Task<ShufflingParticipantsReply> GetShufflingParticipants(ulong shuffling, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Shuffling, shuffling.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<ShufflingParticipantsReply>("getShufflingParticipants", queryParameters);
        }

        public async Task<TransactionCreatedReply> ShufflingCancel(ulong shuffling, Account cancellingAccount,
            BinaryHexString shufflingStateHash, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Shuffling, shuffling.ToString()},
                {Parameters.CancellingAccount, cancellingAccount.ToString()},
                {Parameters.ShufflingStateHash, shufflingStateHash.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("shufflingCancel", queryParameters);
        }

        public async Task<TransactionCreatedReply> ShufflingCreate(Amount amount, int participantCount, int registrationPeriod,
            CreateTransactionParameters parameters, ulong? holding = null, HoldingType? holdingType = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Amount, amount.Nqt.ToString()},
                {Parameters.ParticipantCount, participantCount.ToString()},
                {Parameters.RegistrationPeriod, registrationPeriod.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            queryParameters.AddIfHasValue(Parameters.Holding, holding);
            queryParameters.AddIfHasValue(Parameters.HoldingType, holdingType != null ? (int?) holdingType : null);
            return await Post<TransactionCreatedReply>("shufflingCreate", queryParameters);
        }

        public async Task<TransactionCreatedReply> ShufflingProcess(ulong shuffling, string recipientSecretPhrase,
            BinaryHexString recipientPublicKey, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Shuffling, shuffling.ToString()},
                {Parameters.RecipientSecretPhrase, recipientSecretPhrase},
                {Parameters.RecipientPublicKey, recipientPublicKey.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("shufflingProcess", queryParameters);
        }

        public async Task<TransactionCreatedReply> ShufflingRegister(BinaryHexString shufflingFullHash, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.ShufflingFullHash, shufflingFullHash.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("shufflingRegister", queryParameters);
        }

        public async Task<TransactionCreatedReply> ShufflingVerify(ulong shuffling, BinaryHexString shufflingStateHash, CreateTransactionParameters parameters)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.Shuffling, shuffling.ToString()},
                {Parameters.ShufflingStateHash, shufflingStateHash.ToString()}
            };
            parameters.AppendToQueryParameters(queryParameters);
            return await Post<TransactionCreatedReply>("shufflingVerify", queryParameters);
        }

        public async Task<ShufflerReply> StartShuffler(string secretPhrase, BinaryHexString shufflingFullHash,
            string recipientSecretPhrase, BinaryHexString recipientPublicKey = null)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {Parameters.SecretPhrase, secretPhrase},
                {Parameters.ShufflingFullHash, shufflingFullHash.ToHexString()},
                {Parameters.RecipientSecretPhrase, recipientSecretPhrase}
            };
            queryParameters.AddIfHasValue(Parameters.RecipientPublicKey, recipientPublicKey);
            return await Post<ShufflerReply>("startShuffler", queryParameters);
        }

        public async Task<StopShufflerReply> StopShuffler(Account account = null, BinaryHexString shufflingFullHash = null,
            SecretPhraseOrAdminPassword secretPhraseOrAdminPassword = null)
        {
            var queryParameters = secretPhraseOrAdminPassword == null
                ? new Dictionary<string, string>()
                : secretPhraseOrAdminPassword.QueryParameters;

            queryParameters.AddIfHasValue(Parameters.Account, account);
            queryParameters.AddIfHasValue(Parameters.ShufflingFullHash, shufflingFullHash);
            return await Post<StopShufflerReply>("stopShuffler", queryParameters);
        }
    }
}
