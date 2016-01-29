namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customeradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Gender = c.String(),
                        PostalCode = c.String(),
                        ApplicationUserID = c.String(maxLength: 128),
                        RequireUniqueEmail = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "ApplicationUserID" });
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.Customers");
        }
    }
}
