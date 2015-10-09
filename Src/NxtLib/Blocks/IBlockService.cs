using System;
using System.Threading.Tasks;

namespace NxtLib.Blocks
{
    public interface IBlockService
    {
        Task<GetBlockReply<ulong>> GetBlock(BlockLocator query, bool? includeExecutedPhased = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetBlockIdReply> GetBlockId(int height, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query,
            bool? includeExecutedPhased = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BlocksReply<ulong>> GetBlocks(int? firstindex = null, int? lastindex = null, DateTime? timestamp = null,
            bool? includeExecutedPhased = null, ulong? requireBlock = null, ulong? requireLastBlock = null);

        Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstindex = null, int? lastindex = null,
            DateTime? timestamp = null, bool? includeExecutedPhased = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);

        Task<GetEcBlockReply> GetEcBlock(DateTime? timestamp = null, ulong? requireBlock = null,
            ulong? requireLastBlock = null);
    }
}