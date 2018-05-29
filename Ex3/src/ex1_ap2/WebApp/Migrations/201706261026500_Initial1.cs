namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Losts", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "CountGamesCompiting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CountGamesCompiting", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Losts");
        }
    }
}
