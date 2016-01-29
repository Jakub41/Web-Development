namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsComments", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductsComments", new[] { "ProductID" });
            DropTable("dbo.ProductsComments");
        }
    }
}
