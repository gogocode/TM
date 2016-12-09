﻿using System;
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

        }

        public virtual DbSet<User> Users { get; set; } //使用者
        public virtual DbSet<Role> Roles { get; set; } //角色
        public virtual DbSet<Catalog> Catalogs { get; set; } //角色

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
