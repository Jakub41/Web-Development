namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailconfirm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConfirmationToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmationToken");
        }
    }
}
