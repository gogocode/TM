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
        private readonly bool isInitDBData = true;

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

        protected void InitDBData(TM.Domain.Models.TMDbContext db)
        {
            try
            { 

                //使用者
                User superadmin = new User() { UserName = "超級管理員", Account = "superadmin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                User admin = new User() { UserName = "管理員", Account = "admin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                db.Users.Add(superadmin);
                db.Users.Add(admin);

                //目錄權限
                Catalog catalogLevel01 = new Catalog();
                catalogLevel01 = new Catalog { CatalogName = "目錄維護(SuperAdmin Only)", CatalogOrder = 1, CatalogCode = "01", IsMenu = true, MenuControllerName = "Catalogs", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId = null, Comment = "" };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "首頁",              CatalogOrder = 1, CatalogCode = "0100", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "新增",              CatalogOrder = 2, CatalogCode = "0101", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "修改",              CatalogOrder = 3, CatalogCode = "0102", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "刪除",              CatalogOrder = 4, CatalogCode = "0103", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "角色權限修改",       CatalogOrder = 5, CatalogCode = "0104", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "使用者權限修改",     CatalogOrder = 6, CatalogCode = "0105", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                }.ForEach(o => db.Catalogs.Add(o));

                //角色
                new List<Role> {
                    new Role {  RoleName = "超級管理員", Users = new List<User>() { superadmin } },
                    new Role {  RoleName = "管理員", Users = new List<User>() { admin } },
                }.ForEach(o => db.Roles.Add(o));

                


                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
