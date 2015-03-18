using System.Data.Entity.Migrations;

namespace NxtExchange.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockchainStatus",
                c => new
                    {
                        Id = c.Int(false, true),
                        LastConfirmedBlockId = c.Long(false),
                        LastKnownBlockId = c.Long(false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InboundTransaction",
                c => new
                    {
                        Id = c.Int(false, true),
                        TransactionId = c.Long(false),
                        CustomerId = c.Int(false),
                        AmountNqt = c.Long(false),
                        Status = c.Int(false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InboundTransaction");
            DropTable("dbo.BlockchainStatus");
        }
    }
}
