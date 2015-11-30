using System;
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

        public Task<object> GetAccountShufflings(Account account, bool includeFinished, bool includeHoldingInfo,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public async Task<GetShufflingsReply> GetAllShufflings(bool? includeFinished = null, bool? includeHoldingInfo = null, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.IncludeFinished, includeFinished);
            queryParameters.AddIfHasValue(Parameters.IncludeHoldingInfo, includeHoldingInfo);
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetShufflingsReply>("getAllShufflings", queryParameters);
        }

        public Task<object> GetAssignedShufflings(Account account, bool includeHoldingInfo, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetHoldingShufflings(long holding, int stage, bool includeFinished, int? firstIndex = null,
            int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetShufflers(Account account, BinaryHexString shufflingFullHash,
            SecretPhraseOrAdminPassword sercretPhraseOrAdminPassword)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetShuffling(long shuffling, bool includeHoldingInfo, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetShufflingParticipants(long shuffling, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            throw new NotImplementedException();
        }

        public Task<object> ShufflingCancel(long shuffling, Account cancellingAccount,
            BinaryHexString shufflingStateHash, CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> ShufflingCreate(long holding, object holdingType, long amount, int participantCount,
            int registrationPeriod, CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> ShufflingProcess(long shuffling, string recipientSecretPhrase,
            BinaryHexString recipientPublicKey, CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> ShufflingRegister(BinaryHexString shufflingFullHash, CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> ShufflingVerify(long shuffling, BinaryHexString shufflingStateHash,
            CreateTransactionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<object> StartShuffler(string secretPhrase, BinaryHexString shufflingFullHash,
            string recipientSecretPhrase, BinaryHexString recipientPublicKey)
        {
            throw new NotImplementedException();
        }

        public Task<object> StopShuffler(Account account, BinaryHexString shufflingFullHash,
            SecretPhraseOrAdminPassword secretPhraseOrAdminPassword)
        {
            throw new NotImplementedException();
        }
    }
}
