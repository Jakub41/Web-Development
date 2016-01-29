namespace ComputerWebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetpassword1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Retrievepasswords", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Retrievepasswords", "Email");
        }
    }
}
