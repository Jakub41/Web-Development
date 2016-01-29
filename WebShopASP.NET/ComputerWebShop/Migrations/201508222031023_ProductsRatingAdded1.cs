namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsRatingAdded1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductRatingViewModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductRatings = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Products_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Products_ID)
                .Index(t => t.Products_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductRatingViewModels", "Products_ID", "dbo.Products");
            DropIndex("dbo.ProductRatingViewModels", new[] { "Products_ID" });
            DropTable("dbo.ProductRatingViewModels");
        }
    }
}
