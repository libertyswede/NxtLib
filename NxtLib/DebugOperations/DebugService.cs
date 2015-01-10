using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.DebugOperations
{
    public interface IDebugService
    {
        Task<DebugReply> ClearUnconfirmedTransactions();
        Task<DebugReply> FullReset();
        Task<DebugReply> LuceneReindex();
        Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator);
        Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null);
    }

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

        public async Task<DebugReply> ClearUnconfirmedTransactions()
        {
            return await Post<DebugReply>("clearUnconfirmedTransactions");
        }

        public async Task<DebugReply> FullReset()
        {
            return await Post<DebugReply>("fullReset");
        }

        public async Task<DebugReply> LuceneReindex()
        {
            return await Post<DebugReply>("luceneReindex");
        }

        public async Task<BlocksReply<Transaction>> PopOff(HeightOrNumberOfBlocksLocator locator)
        {
            return await Post<BlocksReply<Transaction>>("popOff", locator.QueryParameters);
        }

        public async Task<ScanReply> Scan(HeightOrNumberOfBlocksLocator locator, bool? validate = null)
        {
            var queryParameters = locator.QueryParameters;
            AddToParametersIfHasValue("validate", validate, queryParameters);
            return await Post<ScanReply>("scan", queryParameters);
        }
    }
}
