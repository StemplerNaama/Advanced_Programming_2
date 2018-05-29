namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CountGamesCompiting", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Wins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Wins", c => c.String());
            AlterColumn("dbo.Users", "CountGamesCompiting", c => c.String());
        }
    }
}
