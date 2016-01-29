namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentInfoes", "ExpiryDate", c => c.String());
            DropColumn("dbo.PaymentInfoes", "ExpiryMonth");
            DropColumn("dbo.PaymentInfoes", "ExpiryYear");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentInfoes", "ExpiryYear", c => c.String());
            AddColumn("dbo.PaymentInfoes", "ExpiryMonth", c => c.String());
            DropColumn("dbo.PaymentInfoes", "ExpiryDate");
        }
    }
}
