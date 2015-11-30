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
    }
}