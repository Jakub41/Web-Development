namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expiryyearAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "ExpiryYear", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentInfoes", "ExpiryYear");
        }
    }
}
