using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace NxtExchange.DAL
{
    public interface INxtRepository
    {
        Task<BlockchainStatus> GetBlockchainStatus();
        Task<InboundTransaction> GetInboundTransaction(long transactionId);
        Task AddInboundTransaction(InboundTransaction transaction);
        Task UpdateTransactionStatus(long transactionId, TransactionStatus status);
        Task<List<InboundTransaction>>  GetInboundTransactions(IEnumerable<long> transactionIds);
        Task<List<InboundTransaction>> GetNonSecuredTransactions();
        Task UpdateBlockchainStatus(BlockchainStatus blockchainStatus);
    }

    public class NxtRepository : INxtRepository
    {
        public async Task<BlockchainStatus> GetBlockchainStatus()
        {
            using (var context = new NxtContext())
            {
                var blockchainStatus = await context.BlockchainStatus.SingleAsync();

                // Workaround to get datetime values into utc kind
                blockchainStatus.LastKnownBlockTimestamp = ConvertToUtc(blockchainStatus.LastKnownBlockTimestamp);
                blockchainStatus.LastConfirmedBlockTimestamp = ConvertToUtc(blockchainStatus.LastConfirmedBlockTimestamp);
                blockchainStatus.LastSecureBlockTimestamp = ConvertToUtc(blockchainStatus.LastSecureBlockTimestamp);

                return blockchainStatus;
            }
        }

        /// <summary>
        /// Convert the datetime into utc kind, while maintaining the same time as before.
        /// </summary>
        private static DateTime ConvertToUtc(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);
        }

        public async Task<InboundTransaction> GetInboundTransaction(long transactionId)
        {
            using (var context = new NxtContext())
            {
                return await context.InboundTransactions.SingleOrDefaultAsync(t => t.TransactionId == transactionId);
            }
        }

        public async Task AddInboundTransaction(InboundTransaction transaction)
        {
            using (var context = new NxtContext())
            {
                context.InboundTransactions.Add(transaction);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateTransactionStatus(long transactionId, TransactionStatus status)
        {
            using (var context = new NxtContext())
            {
                var dbTransaction = await context.InboundTransactions.SingleAsync(t => t.TransactionId == transactionId);
                dbTransaction.Status = status;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<InboundTransaction>> GetInboundTransactions(IEnumerable<long> transactionIds)
        {
            using (var context = new NxtContext())
            {
                return await context.InboundTransactions.Where(t => transactionIds.Contains(t.TransactionId)).ToListAsync();
            }
        }

        public async Task<List<InboundTransaction>> GetNonSecuredTransactions()
        {
            using (var context = new NxtContext())
            {
                return await context.InboundTransactions.Where(t => t.Status != TransactionStatus.Secured).ToListAsync();
            }
        }

        public async Task UpdateBlockchainStatus(BlockchainStatus blockchainStatus)
        {
            using (var context = new NxtContext())
            {
                context.BlockchainStatus.Attach(blockchainStatus);
                context.Entry(blockchainStatus).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
