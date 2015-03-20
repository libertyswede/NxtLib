using NxtExchange.DAL;

namespace NxtExchange.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NxtExchange.DAL.NxtContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NxtExchange.DAL.NxtContext context)
        {
            const long genesisBlockId = 2680262203532249785;
            var genesisBlockTimestamp = new DateTime(2013, 11, 24, 12, 0, 0, DateTimeKind.Utc);

            context.BlockchainStatus.Add(new BlockchainStatus
            {
                LastKnownBlockId = genesisBlockId,
                LastKnownBlockTimestamp = genesisBlockTimestamp,

                LastConfirmedBlockId = genesisBlockId,
                LastConfirmedBlockTimestamp = genesisBlockTimestamp,

                LastSecureBlockId = genesisBlockId,
                LastSecureBlockTimestamp = genesisBlockTimestamp
            });
        }
    }
}
