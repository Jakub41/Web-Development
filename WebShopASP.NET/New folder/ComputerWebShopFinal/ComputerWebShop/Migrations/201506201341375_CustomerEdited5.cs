namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerEdited5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "State", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Country", c => c.String());
            AlterColumn("dbo.Customers", "State", c => c.String());
            AlterColumn("dbo.Customers", "PostalCode", c => c.String());
            AlterColumn("dbo.Customers", "City", c => c.String());
            AlterColumn("dbo.Customers", "Gender", c => c.String());
            AlterColumn("dbo.Customers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "LastName", c => c.String());
        }
    }
}
