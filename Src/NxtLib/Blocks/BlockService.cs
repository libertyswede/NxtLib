using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.Blocks
{
    public class BlockService : BaseService, IBlockService
    {
        public BlockService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public BlockService(IDateTimeConverter dateTimeConverter)
            : base(dateTimeConverter)
        {
        }

        public async Task<GetBlockReply<ulong>> GetBlock(BlockLocator query)
        {
            return await Get<GetBlockReply<ulong>>("getBlock", query.QueryParameters);
        }

        public async Task<GetBlockIdReply> GetBlockId(int height)
        {
            var queryParameters = new Dictionary<string, string> { { "height", height.ToString() } };
            return await Get<GetBlockIdReply>("getBlockId", queryParameters);
        }

        public async Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.Add("includeTransactions", "true");
            return await Get<GetBlockReply<Transaction>>("getBlock", queryParameters);
        }

        public async Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstindex = null,
            int? lastindex = null, DateTime? timestamp = null)
        {
            var queryParameters = new Dictionary<string, string> {{"includeTransactions", "true"}};
            queryParameters.AddIfHasValue("firstIndex", firstindex);
            queryParameters.AddIfHasValue("lastIndex", lastindex);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            return await Get<BlocksReply<Transaction>>("getBlocks", queryParameters);
        }

        public async Task<BlocksReply<ulong>> GetBlocks(int? firstindex = null, int? lastindex = null,
            DateTime? timestamp = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue("firstIndex", firstindex);
            queryParameters.AddIfHasValue("lastIndex", lastindex);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            return await Get<BlocksReply<ulong>>("getBlocks", queryParameters);
        }

        public async Task<GetEcBlockReply> GetEcBlock(DateTime? timestamp = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue(timestamp, queryParameters);
            return await Get<GetEcBlockReply>("getECBlock", queryParameters);
        }
    }
}
