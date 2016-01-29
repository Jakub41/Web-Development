namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartaddedForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Carts", "ProductID");
            AddForeignKey("dbo.Carts", "ProductID", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "ProductID", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductID" });
        }
    }
}
