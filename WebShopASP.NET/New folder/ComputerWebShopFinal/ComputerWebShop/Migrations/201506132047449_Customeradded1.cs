namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customeradded1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "RequireUniqueEmail");
            DropColumn("dbo.AspNetUsers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.Customers", "RequireUniqueEmail", c => c.Boolean(nullable: false));
        }
    }
}
