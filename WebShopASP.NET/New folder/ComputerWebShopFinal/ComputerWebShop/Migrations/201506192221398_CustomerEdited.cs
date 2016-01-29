namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerEdited : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "PostalCode");
            DropColumn("dbo.Customers", "Company");
            DropColumn("dbo.Customers", "Zip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Zip", c => c.String());
            AddColumn("dbo.Customers", "Company", c => c.String());
            AddColumn("dbo.Customers", "PostalCode", c => c.String());
        }
    }
}
