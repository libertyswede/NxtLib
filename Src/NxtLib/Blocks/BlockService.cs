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

        public async Task<GetBlockReply<ulong>> GetBlock(BlockLocator query, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GetBlockReply<ulong>>("getBlock", queryParameters);
        }

        public async Task<GetBlockIdReply> GetBlockId(int height, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"height", height.ToString()}};
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GetBlockIdReply>("getBlockId", queryParameters);
        }

        public async Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query,
            bool? includeExecutedPhased = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.Add("includeTransactions", "true");
            AddToParametersIfHasValue("includeExecutedPhased", includeExecutedPhased, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GetBlockReply<Transaction>>("getBlock", queryParameters);
        }

        public async Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstindex = null,
            int? lastindex = null, DateTime? timestamp = null, bool? includeExecutedPhased = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"includeTransactions", "true"}};
            queryParameters.AddIfHasValue("firstIndex", firstindex);
            queryParameters.AddIfHasValue("lastIndex", lastindex);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            AddToParametersIfHasValue("includeExecutedPhased", includeExecutedPhased, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<BlocksReply<Transaction>>("getBlocks", queryParameters);
        }

        public async Task<BlocksReply<ulong>> GetBlocks(int? firstindex = null, int? lastindex = null,
            DateTime? timestamp = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue("firstIndex", firstindex);
            queryParameters.AddIfHasValue("lastIndex", lastindex);
            AddToParametersIfHasValue("timestamp", timestamp, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<BlocksReply<ulong>>("getBlocks", queryParameters);
        }

        public async Task<GetEcBlockReply> GetEcBlock(DateTime? timestamp = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            AddToParametersIfHasValue(timestamp, queryParameters);
            AddToParametersIfHasValue("requireBlock", requireBlock, queryParameters);
            AddToParametersIfHasValue("requireLastBlock", requireLastBlock, queryParameters);
            return await Get<GetEcBlockReply>("getECBlock", queryParameters);
        }
    }
}