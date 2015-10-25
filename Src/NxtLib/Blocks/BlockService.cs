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
            queryParameters.AddIfHasValue(nameof(includeExecutedPhased), includeExecutedPhased);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetBlockReply<ulong>>("getBlock", queryParameters);
        }

        public async Task<GetBlockIdReply> GetBlockId(int height, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{nameof(height), height.ToString()}};
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetBlockIdReply>("getBlockId", queryParameters);
        }

        public async Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query,
            bool? includeExecutedPhased = null, ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = query.QueryParameters;
            queryParameters.Add("includeTransactions", "true");
            queryParameters.AddIfHasValue(nameof(includeExecutedPhased), includeExecutedPhased);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetBlockReply<Transaction>>("getBlock", queryParameters);
        }

        public async Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstIndex = null,
            int? lastIndex = null, DateTime? timestamp = null, bool? includeExecutedPhased = null,
            ulong? requireBlock = null, ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string> {{"includeTransactions", "true"}};
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(includeExecutedPhased), includeExecutedPhased);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<BlocksReply<Transaction>>("getBlocks", queryParameters);
        }

        public async Task<BlocksReply<ulong>> GetBlocks(int? firstIndex = null, int? lastIndex = null,
            DateTime? timestamp = null, bool? includeExecutedPhased = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(nameof(firstIndex), firstIndex);
            queryParameters.AddIfHasValue(nameof(lastIndex), lastIndex);
            queryParameters.AddIfHasValue(nameof(timestamp), timestamp);
            queryParameters.AddIfHasValue(nameof(includeExecutedPhased), includeExecutedPhased);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<BlocksReply<ulong>>("getBlocks", queryParameters);
        }

        public async Task<GetEcBlockReply> GetEcBlock(DateTime? timestamp = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null)
        {
            var queryParameters = new Dictionary<string, string>();
            queryParameters.AddIfHasValue(timestamp);
            queryParameters.AddIfHasValue(nameof(requireBlock), requireBlock);
            queryParameters.AddIfHasValue(nameof(requireLastBlock), requireLastBlock);
            return await Get<GetEcBlockReply>("getECBlock", queryParameters);
        }
    }
}