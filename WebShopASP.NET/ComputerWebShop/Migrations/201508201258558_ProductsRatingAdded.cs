namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsRatingAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsRatings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductRatings = c.Int(nullable: false),
                        ApplicationUserID = c.String(maxLength: 128),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .Index(t => t.ApplicationUserID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsRatings", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductsRatings", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductsRatings", new[] { "ProductID" });
            DropIndex("dbo.ProductsRatings", new[] { "ApplicationUserID" });
            DropTable("dbo.ProductsRatings");
        }
    }
}
