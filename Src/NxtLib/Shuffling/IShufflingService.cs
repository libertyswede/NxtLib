using System.Threading.Tasks;

namespace NxtLib.Shuffling
{
    public interface IShufflingService
    {
        Task<GetShufflingsReply> GetAccountShufflings(Account account, bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetShufflingsReply> GetAllShufflings(bool? includeFinished = null,
            bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetShufflingsReply> GetAssignedShufflings(Account account, bool? includeHoldingInfo = null,
            int? firstIndex = null, int? lastIndex = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetShufflingsReply> GetHoldingShufflings(long? holding = null, ShufflingStage? stage = null,
            bool? includeFinished = null, bool? includeHoldingInfo = null, int? firstIndex = null, int? lastIndex = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null);
    }
}