namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CartID = c.String(),
                        Total = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUserID" });
            DropTable("dbo.Orders");
        }
    }
}
