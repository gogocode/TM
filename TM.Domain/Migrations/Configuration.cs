namespace TM.Domain.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<TM.Domain.Models.TMDbContext>
    {
        private readonly bool isInitDBData = false;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TM.Domain.Models.TMDbContext context)
        {
            if (isInitDBData) {
                InitDBData(context);
            }
        }

        protected void InitDBData(TM.Domain.Models.TMDbContext context)
        {
            context.Users.AddOrUpdate(
            new User { UserName = "超級管理員", Account = "superadmin", Password = "1234", Email = "tonyho@86shop.com.tw", IsActive = true },
            new User { UserName = "管理員", Account = "admin", Password = "1234", Email = "tonyho@86shop.com.tw", IsActive = true }
                    );

            context.SaveChanges();
        }
    }
}
