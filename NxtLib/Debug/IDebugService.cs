using System.Threading.Tasks;

namespace NxtLib.Debug
{
    public interface IDebugService
    {
        Task<DoneReply> ClearUnconfirmedTransactions();
        Task<DoneReply> FullReset();
        Task<DoneReply> LuceneReindex();
        Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator);
        Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null);
    }
}