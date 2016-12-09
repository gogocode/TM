using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;

namespace TM.Domain
{
    public class Configuration : DbMigrationsConfiguration<TM.Domain.Models.TMDbContext>
    {
        private readonly bool _pendingMigrations;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            // Check if there are migrations pending to run, this can happen if database
            // doesn't exists or if there was any change in the schema

            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();

            // If there are pending migrations run migrator.Update() to create/update the
            // database then run the Seed() method to populate the data if necessary.
            if (_pendingMigrations)
            {
                migrator.Update();
                Seed(new TMDbContext());
            }
        }

        protected override void Seed(TMDbContext context)
        {
            // Your seed code here...
            new List<User> {
                new User {  UserName = "超級管理員", Account = "superadmin", Password = "1234" , Email = "tonyho@86shop.com.tw", IsActive = true },
                new User {  UserName = "管理員", Account = "admin", Password = "1234" , Email = "tonyho@86shop.com.tw", IsActive = true },
            }.ForEach(o => context.Users.Add(o));

            // Make sure to have the context save changes and to call the base seed method afterwards.
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
