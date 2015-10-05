using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NxtLib.Debug
{
    public interface IDebugService
    {
        Task<DoneReply> ClearUnconfirmedTransactions();

        Task<DumpPeersReply> DumpPeers(string version = null, int? weight = null, bool? connect = null,
            string adminPassword = null);

        Task<DoneReply> FullReset();

        Task<TransactionsListReply> GetAllBroadcastedTransactions(ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<TransactionsListReply> GetAllWaitingTransactions(ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<LogReply> GetLog(int count);

        Task<JObject> GetStackTraces(int? depth = null);

        Task<DoneReply> LuceneReindex();

        Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator);

        Task<DoneReply> RebroadcastUnconfirmedTransactions();

        Task<DoneReply> RequeueUnconfirmedTransactions();

        Task<RetrievePrunedDataReply> RetrievePrunedData();

        Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null);

        Task<SetLoggingReply> SetLogging(string logLevel, IEnumerable<string> communicationEvents);

        Task<ShutdownReply> Shutdown(bool? scan = null);

        Task<DoneReply> TrimDerivedTables();
    }
}