using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Models
{
    public class TMDbContext : DbContext
    {
        public TMDbContext() : base("name=TMDbConnectionString")
        {
            // Create the database and tables if it doesn't exist.
            Database.SetInitializer(new TMDbInitializer());
            Database.Initialize(true);

            //Disable initializer
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TMDbContext, Migrations.Configuration>("TMDbConnectionString"));
        }

        public virtual DbSet<User> Users { get; set; } //使用者
        public virtual DbSet<Role> Roles { get; set; } //角色
        public virtual DbSet<Catalog> Catalogs { get; set; } //角色
        public virtual DbSet<Diary> Diaries { get; set; } //工作日誌
        public virtual DbSet<Item> Items { get; set; } //項目

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
