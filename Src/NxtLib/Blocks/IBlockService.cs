using System;
using System.Threading.Tasks;

namespace NxtLib.Blocks
{
    public interface IBlockService
    {
        Task<GetBlockReply<ulong>> GetBlock(BlockLocator query);
        Task<GetBlockIdReply> GetBlockId(int height);
        Task<GetBlockReply<Transaction>> GetBlockIncludeTransactions(BlockLocator query);
        Task<BlocksReply<ulong>> GetBlocks(int? firstindex = null, int? lastindex = null, DateTime? timestamp = null);
        Task<BlocksReply<Transaction>> GetBlocksIncludeTransactions(int? firstindex = null, int? lastindex = null,
            DateTime? timestamp = null);
        Task<GetEcBlockReply> GetEcBlock(DateTime? timestamp = null);
    }
}