using System.Data.Entity.Migrations;
using NxtExchange.DAL;

namespace NxtExchange.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<NxtContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NxtExchange.DAL.NxtContext";
        }

        protected override void Seed(NxtContext context)
        {
            context.BlockchainStatus.Add(new BlockchainStatus
            {
                LastConfirmedBlockId = 2680262203532249785, 
                LastKnownBlockId = 2680262203532249785
            });
        }
    }
}
