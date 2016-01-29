namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetpassword : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Retrievepasswords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConfirmationToken = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Retrievepasswords");
        }
    }
}
