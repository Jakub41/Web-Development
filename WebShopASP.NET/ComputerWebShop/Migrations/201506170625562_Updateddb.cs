namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateddb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "ApplicationUserID" });
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CartNumber = c.String(),
                        ExpiryMonth = c.String(),
                        SecurityCode = c.String(),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            AddColumn("dbo.Customers", "City", c => c.String());
            AddColumn("dbo.Customers", "Company", c => c.String());
            AddColumn("dbo.Customers", "State", c => c.String());
            AddColumn("dbo.Customers", "Country", c => c.String());
            AddColumn("dbo.Customers", "Zip", c => c.String());
            AddColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "ID", cascadeDelete: true);
            DropColumn("dbo.Orders", "ApplicationUserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ApplicationUserID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.PaymentInfoes", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.PaymentInfoes", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropColumn("dbo.Orders", "CustomerID");
            DropColumn("dbo.Customers", "Zip");
            DropColumn("dbo.Customers", "Country");
            DropColumn("dbo.Customers", "State");
            DropColumn("dbo.Customers", "Company");
            DropColumn("dbo.Customers", "City");
            DropTable("dbo.PaymentInfoes");
            CreateIndex("dbo.Orders", "ApplicationUserID");
            AddForeignKey("dbo.Orders", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
