using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NxtLib.Debug
{
    public interface IDebugService
    {
        Task<DoneReply> ClearUnconfirmedTransactions();
        Task<DumpPeersReply> DumpPeers(string version);
        Task<DoneReply> FullReset();
        Task<TransactionsListReply> GetAllBroadcastedTransactions();
        Task<TransactionsListReply> GetAllWaitingTransactions();
        Task<LogReply> GetLog(int count);
        Task<JObject> GetStackTraces();
        Task<DoneReply> LuceneReindex();
        Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator);
        Task<DoneReply> RebroadcastUnconfirmedTransactions();
        Task<DoneReply> RequeueUnconfirmedTransactions();
        Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null);
    }
}