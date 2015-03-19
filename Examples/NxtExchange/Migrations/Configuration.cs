using System.Data.Entity.Migrations;
using NxtExchange.DAL;

namespace NxtExchange.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NxtContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NxtContext context)
        {
            context.BlockchainStatus.Add(new BlockchainStatus
            {
                LastSecureBlockId = 2680262203532249785,
                LastKnownBlockId = 2680262203532249785
            });
        }
    }
}
