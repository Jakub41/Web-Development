namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RetrieveDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductRatingViewModels", "Products_ID", "dbo.Products");
            DropIndex("dbo.ProductRatingViewModels", new[] { "Products_ID" });
            DropTable("dbo.ProductRatingViewModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ProductRatingViewModels", "Products_ID");
            AddForeignKey("dbo.ProductRatingViewModels", "Products_ID", "dbo.Products", "ID");
        }
    }
}
