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
            : base(baseAddress)
        {
        }

        public async Task<GetBlockReply<ulong>> GetBlock(BlockLocator query, bool? includeExecutedPhased = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.AddIfHasValue(Parameters.IncludeExecutedPhased, includeExecutedPhased);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetBlockReply<ulong>>("getBlock", queryParameters);
        }

        public async Task<GetBlockIdReply> GetBlockId(int height, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.Height, height.ToString()}};
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetBlockIdReply>("getBlockId", queryParameters);
        }

        public async Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query,
            bool? includeExecutedPhased = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.Add(Parameters.IncludeTransactions, true.ToString());
            queryParameters.AddIfHasValue(Parameters.IncludeExecutedPhased, includeExecutedPhased);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetBlockReply<Transaction>>("getBlock", queryParameters);
        }

        public async Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeExecutedPhased = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{Parameters.IncludeTransactions, true.ToString()}};
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.IncludeExecutedPhased, includeExecutedPhased);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<BlocksReply<Transaction>>("getBlocks", queryParameters);
        }

        public async Task<BlocksReply<ulong>> GetBlocks(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeExecutedPhased = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.FirstIndex, firstIndex);
            queryParameters.AddIfHasValue(Parameters.LastIndex, lastIndex);
            queryParameters.AddIfHasValue(Parameters.Timestamp, timestamp);
            queryParameters.AddIfHasValue(Parameters.IncludeExecutedPhased, includeExecutedPhased);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<BlocksReply<ulong>>("getBlocks", queryParameters);
        }

        public async Task<GetEcBlockReply> GetEcBlock(DateTime? timeStamp = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(Parameters.Timestamp, timeStamp);
            queryParameters.AddIfHasValue(Parameters.RequireBlock, requireBlock);
            queryParameters.AddIfHasValue(Parameters.RequireLastBlock, requireLastBlock);
            return await Get<GetEcBlockReply>("getECBlock", queryParameters);
        }
    }
}