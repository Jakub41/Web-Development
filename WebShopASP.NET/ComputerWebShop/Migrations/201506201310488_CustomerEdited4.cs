namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerEdited4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PostalCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PostalCode");
        }
    }
}
