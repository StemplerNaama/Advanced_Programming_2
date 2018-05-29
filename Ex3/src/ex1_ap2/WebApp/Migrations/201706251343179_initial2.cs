namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CountGamesCompiting", c => c.String());
            AddColumn("dbo.Users", "Wins", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Wins");
            DropColumn("dbo.Users", "CountGamesCompiting");
        }
    }
}
