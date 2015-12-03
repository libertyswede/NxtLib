using System.Threading.Tasks;

namespace NxtLib.Shuffling
{
    public interface IShufflingService
    {
        Task<ShufflingsReply> GetAccountShufflings(Account account, bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<ShufflingsReply> GetAllShufflings(bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<ShufflingsReply> GetAssignedShufflings(Account account, bool? includeHoldingInfo = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<ShufflingsReply> GetHoldingShufflings(ulong? holding = null, ShufflingStage? stage = null,
            bool? includeFinished = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, 
            ulong? requireLastBlock = null);

        Task<ShufflersReply> GetShufflers(Account account = null, BinaryHexString shufflingFullHash = null,
            SecretPhraseOrAdminPassword sercretPhraseOrAdminPassword = null);

        Task<ShufflingReply> GetShuffling(ulong shuffling, bool? includeHoldingInfo = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<ShufflingParticipantsReply> GetShufflingParticipants(ulong shuffling, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<object> ShufflingCancel(ulong shuffling, Account cancellingAccount,
            BinaryHexString shufflingStateHash, CreateTransactionParameters parameters);

        Task<object> ShufflingCreate(ulong holding, object holdingType, long amount, int participantCount,
            int registrationPeriod, CreateTransactionParameters parameters);

        Task<object> ShufflingProcess(ulong shuffling, string recipientSecretPhrase,
            BinaryHexString recipientPublicKey, CreateTransactionParameters parameters);

        Task<object> ShufflingRegister(BinaryHexString shufflingFullHash, CreateTransactionParameters parameters);

        Task<object> ShufflingVerify(ulong shuffling, BinaryHexString shufflingStateHash,
            CreateTransactionParameters parameters);

        Task<object> StartShuffler(string secretPhrase, BinaryHexString shufflingFullHash,
            string recipientSecretPhrase, BinaryHexString recipientPublicKey);

        Task<object> StopShuffler(Account account, BinaryHexString shufflingFullHash,
            SecretPhraseOrAdminPassword secretPhraseOrAdminPassword);
    }
}