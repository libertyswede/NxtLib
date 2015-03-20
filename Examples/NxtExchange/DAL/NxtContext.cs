using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NxtExchange.DAL
{
    public class NxtContext : DbContext
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
