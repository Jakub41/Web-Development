namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProdDesc", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProdDesc", c => c.String());
        }
    }
}
