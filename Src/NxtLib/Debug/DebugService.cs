using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Debug
{
    public class DebugService : BaseService, IDebugService
    {
        public DebugService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public DebugService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<DoneReply> ClearUnconfirmedTransactions()
        {
            return await Post<DoneReply>("clearUnconfirmedTransactions");
        }

        public async Task<DumpPeersReply> DumpPeers(string version = null, int? weight = null, bool? connect = null,
            string adminPassword = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("version", version, queryParameters);
            AddToParametersIfHasValue("weight", weight, queryParameters);
            AddToParametersIfHasValue("connect", connect, queryParameters);
            AddToParametersIfHasValue("adminPassword", adminPassword, queryParameters);
            return await Get<DumpPeersReply>("dumpPeers", queryParameters);
        }

        public async Task<DoneReply> FullReset()
        {
            return await Post<DoneReply>("fullReset");
        }

        public async Task<TransactionsListReply> GetAllBroadcastedTransactions(ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TransactionsListReply>("getAllBroadcastedTransactions", queryParameters);
        }

        public async Task<TransactionsListReply> GetAllWaitingTransactions(ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<TransactionsListReply>("getAllWaitingTransactions", queryParameters);
        }

        public async Task<LogReply> GetLog(int count)
        {
            var queryParameters = new Dictionary<string, string> {{"count", count.ToString()}};
            return await Get<LogReply>("logReply", queryParameters);
        }

        // TODO: Implement with proper reply
        public async Task<JObject> GetStackTraces(int? depth = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("depth", depth, queryParameters);
            return await Get("getStackTraces", queryParameters);
        }

        public async Task<DoneReply> LuceneReindex()
        {
            return await Post<DoneReply>("luceneReindex");
        }

        public async Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator)
        {
            return await Post<BlocksReply<Transaction>>("popOff", locator.QueryParameters);
        }

        public async Task<DoneReply> RebroadcastUnconfirmedTransactions()
        {
            return await Post<DoneReply>("rebroadcastUnconfirmedTransactions");
        }

        public async Task<DoneReply> RequeueUnconfirmedTransactions()
        {
            return await Post<DoneReply>("requeueUnconfirmedTransactions");
        }

        public async Task<RetrievePrunedDataReply> RetrievePrunedData()
        {
            return await Post<RetrievePrunedDataReply>("retrievePrunedData");
        }

        public async Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("validate", validate, queryParameters);
            return await Post<ScanReply>("scan", queryParameters);
        }

        public async Task<SetLoggingReply> SetLogging(string logLevel, IEnumerable<string> communicationEvents)
        {
            var communicationEventList = communicationEvents.ToList();
            var queryParameters = new Dictionary<string, List<string>>();
            if (!string.IsNullOrEmpty(logLevel))
            {
                queryParameters.Add("logLevel", new List<string>{logLevel});
            }
            if (communicationEventList.Any())
            {
                queryParameters.Add("communicationEvent", communicationEventList);
            }
            return await Post<SetLoggingReply>("setLogging", queryParameters);
        }

        public async Task<ShutdownReply> Shutdown(bool? scan = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue("scan", scan, queryParameters);
            return await Post<ShutdownReply>("shutdown", queryParameters);
        }

        public async Task<DoneReply> TrimDerivedTables()
        {
            return await Post<DoneReply>("trimDerivedTables");
        }
    }
}
