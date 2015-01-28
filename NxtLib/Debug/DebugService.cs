using System.Threading.Tasks;
using NxtLib.Internal;

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

        public async Task<DoneReply> FullReset()
        {
            return await Post<DoneReply>("fullReset");
        }

        public async Task<DoneReply> LuceneReindex()
        {
            return await Post<DoneReply>("luceneReindex");
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
