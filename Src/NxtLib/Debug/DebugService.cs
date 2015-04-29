using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NxtLib.Internal;

namespace NxtLib.Debug
{
    public class DebugService : BaseService, IDebugService
    {
        public DebugService(string baseAddress = DefaultBaseUrl)
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

        public async Task<DumpPeersReply> DumpPeers(string version)
        {
            var queryParameters = new Dictionary<string, string> {{"version", version}};
            return await Get<DumpPeersReply>("dumpPeers", queryParameters);
        }

        public async Task<DoneReply> FullReset()
        {
            return await Post<DoneReply>("fullReset");
        }

        public async Task<TransactionsListReply> GetAllBroadcastedTransactions()
        {
            return await Get<TransactionsListReply>("getAllBroadcastedTransactions");
        }

        public async Task<TransactionsListReply> GetAllWaitingTransactions()
        {
            return await Get<TransactionsListReply>("getAllWaitingTransactions");
        }

        public async Task<LogReply> GetLog(int count)
        {
            var queryParameters = new Dictionary<string, string> {{"count", count.ToString()}};
            return await Get<LogReply>("logReply", queryParameters);
        }

        // TODO: Implement with proper reply
        public async Task<JObject> GetStackTraces()
        {
            return await Get("getStackTraces");
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

        public async Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("validate", validate, queryParameters);
            return await Post<ScanReply>("scan", queryParameters);
        }
    }
}
