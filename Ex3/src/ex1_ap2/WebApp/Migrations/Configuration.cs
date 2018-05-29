namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Models.WebAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Models.WebAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
           /*  context.Users.AddOrUpdate(p => p.Name,
                  new User() { Name = "Naama Stempler", Email = "naamastempler@gmail.com", Password = "123", CountGamesCompiting = 0, Wins = 0 },
                  new User() { Name = "Eti Yanay", Email = "etiyanay111@gmail.com", Password = "456", CountGamesCompiting = 0, Wins = 0 },
                  new User() { Name = "avi", Email = "avi@gmail.com", Password = "789", CountGamesCompiting = 0, Wins = 0}
                );*/
            //
        }
    }
}
