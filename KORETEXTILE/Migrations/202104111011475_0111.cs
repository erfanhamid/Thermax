namespace BEEERP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0111 : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.SubLedger");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.SubLedger",
            //    c => new
            //        {
            //            SubLedgerID = c.Int(nullable: false),
            //            SubLedgerName = c.String(nullable: false),
            //            SubLedgerType = c.String(nullable: false),
            //            DepotID = c.Int(nullable: false),
            //            ReferenceNo = c.String(),
            //            SLDescription = c.String(),
            //        })
            //    .PrimaryKey(t => t.SubLedgerID);
            
        }
    }
}
