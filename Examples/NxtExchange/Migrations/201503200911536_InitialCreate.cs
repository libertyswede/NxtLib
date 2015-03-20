namespace NxtExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockchainStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastSecureBlockId = c.Long(nullable: false),
                        LastSecureBlockHeight = c.Int(nullable: false),
                        LastKnownBlockId = c.Long(nullable: false),
                        LastKnownBlockHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InboundTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.Long(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DecryptedMessage = c.String(),
                        AmountNqt = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
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
