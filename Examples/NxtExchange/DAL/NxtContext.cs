using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace NxtExchange.DAL
{
    public interface INxtContext : IDisposable
    {
        Task<int> SaveChangesAsync();
        DbSet<BlockchainStatus> BlockchainStatus { get; set; }
        DbSet<InboundTransaction> InboundTransactions { get; set; }
    }

    public class NxtContext : DbContext, INxtContext
    {
        public NxtContext()
            : base("NxtContext")
        {
        }

        public DbSet<BlockchainStatus> BlockchainStatus { get; set; }
        public DbSet<InboundTransaction> InboundTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
