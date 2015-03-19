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
                        LastSecureBlockId = c.Long(false),
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
                        DecryptedMessage = c.String(),
                        AmountNqt = c.Long(false),
                        Status = c.Int(false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TransactionId, unique: true, name: "UQ_TransactionId");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.InboundTransaction", "UQ_TransactionId");
            DropTable("dbo.InboundTransaction");
            DropTable("dbo.BlockchainStatus");
        }
    }
}
