namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryProductAddedUpdated1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Image1");
            DropColumn("dbo.Products", "Image2");
            DropColumn("dbo.Products", "Image3");
            DropColumn("dbo.Products", "Image4");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Image4", c => c.String());
            AddColumn("dbo.Products", "Image3", c => c.String());
            AddColumn("dbo.Products", "Image2", c => c.String());
            AddColumn("dbo.Products", "Image1", c => c.String());
        }
    }
}
