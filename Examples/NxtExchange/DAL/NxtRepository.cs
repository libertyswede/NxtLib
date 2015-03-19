using System.Data.Entity;
using System.Threading.Tasks;
using NxtLib;

namespace NxtExchange.DAL
{
    public interface INxtRepository
    {
        Task<BlockchainStatus> GetBlockchainStatusAsync();
        Task<InboundTransaction> GetInboundTransactionAsync(long transactionId);
        Task AddInboundTransactionAsync(InboundTransaction transaction);
    }

    public class NxtRepository : INxtRepository
    {
        public async Task<BlockchainStatus> GetBlockchainStatusAsync()
        {
            using (var context = new NxtContext())
            {
                return await context.BlockchainStatus.SingleAsync();
            }
        }

        public async Task<InboundTransaction> GetInboundTransactionAsync(long transactionId)
        {
            using (var context = new NxtContext())
            {
                return await context.InboundTransactions.SingleOrDefaultAsync(t => t.TransactionId == transactionId);
            }
        }

        public async Task AddInboundTransactionAsync(InboundTransaction transaction)
        {
            using (var context = new NxtContext())
            {
                context.InboundTransactions.Add(transaction);
                await context.SaveChangesAsync();
            }
        }
    }
}
