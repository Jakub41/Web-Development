namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryProductAddedUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Brand", c => c.String());
            AddColumn("dbo.Products", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "PrimaryImage", c => c.String());
            AddColumn("dbo.Products", "Image1", c => c.String());
            AddColumn("dbo.Products", "Image2", c => c.String());
            AddColumn("dbo.Products", "Image3", c => c.String());
            AddColumn("dbo.Products", "Image4", c => c.String());
            AddColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "TimeAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "TimeAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Products", "Image4");
            DropColumn("dbo.Products", "Image3");
            DropColumn("dbo.Products", "Image2");
            DropColumn("dbo.Products", "Image1");
            DropColumn("dbo.Products", "PrimaryImage");
            DropColumn("dbo.Products", "DateAdded");
            DropColumn("dbo.Products", "Brand");
        }
    }
}
