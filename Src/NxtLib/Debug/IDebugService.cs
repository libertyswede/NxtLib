using System.Threading.Tasks;

namespace NxtLib.Debug
{
    public interface IDebugService
    {
        Task<DoneReply> ClearUnconfirmedTransactions();
        Task<DumpPeersReply> DumpPeers(string version);
        Task<DoneReply> FullReset();
        Task<BroadcastedTransactionsReply> GetAllBroadcastedTransactions();
        Task<DoneReply> LuceneReindex();
        Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator);
        Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null);
    }
}