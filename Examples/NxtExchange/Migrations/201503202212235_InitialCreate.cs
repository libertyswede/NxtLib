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
                        LastKnownBlockId = c.Long(nullable: false),
                        LastKnownBlockTimestamp = c.DateTime(nullable: false),
                        LastConfirmedBlockId = c.Long(nullable: false),
                        LastConfirmedBlockTimestamp = c.DateTime(nullable: false),
                        LastSecureBlockId = c.Long(nullable: false),
                        LastSecureBlockTimestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InboundTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.Long(nullable: false),
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
