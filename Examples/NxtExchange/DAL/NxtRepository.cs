using System.Collections.Generic;
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
        Task UpdateTransactionStatusAsync(long transactionId, TransactionStatus status);
        Task<List<InboundTransaction>>  GetInboundTransactionsAsync(IEnumerable<long> transactionIds);
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

        public async Task UpdateTransactionStatusAsync(long transactionId, TransactionStatus status)
        {
            using (var context = new NxtContext())
            {
                var dbTransaction = await context.InboundTransactions.SingleAsync(t => t.TransactionId == transactionId);
                dbTransaction.Status = status;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<InboundTransaction>> GetInboundTransactionsAsync(IEnumerable<long> transactionIds)
        {
            using (var context = new NxtContext())
            {
                //await context.InboundTransactions
            }
        }
    }
}
