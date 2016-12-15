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

        protected void InitDBData(TM.Domain.Models.TMDbContext db)
        {
            try
            {
                #region �ϥΪ�
                //�ϥΪ�
                User superAdmin = new User() { UserName = "�W�ź޲z��", Account = "superadmin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                User admin = new User() { UserName = "�޲z��", Account = "admin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                User tonyho = new User() { UserName = "tonyho", Account = "1698", Password = "1234", Email = "tonyho@86shop.com.tw", IsActive = true };
                db.Users.Add(superAdmin);
                db.Users.Add(admin);
                db.Users.Add(tonyho);
                #endregion

                #region ����
                //����
                Role superAdminRole = new Role { RoleEngName="superadmin", RoleName = "�W�ź޲z��", Users = new List<User>() { superAdmin } };
                Role adminRole = new Role { RoleEngName="admin", RoleName = "�޲z��", Users = new List<User>() { admin } };
                Role generalRole = new Role { RoleEngName="general", RoleName = "�@��ϥΪ�", Users = new List<User>() { tonyho } };
                db.Roles.Add(superAdminRole);
                db.Roles.Add(adminRole);
                db.Roles.Add(generalRole);
                #endregion

                #region �\����@
                Catalog catalogLevel01 = new Catalog();

                //�\����@�޲z
                catalogLevel01 = new Catalog { CatalogName = "�\����@�޲z", CatalogOrder = 1, Permission = "Catalog", IsMenu = true, Comment = "", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId =null, Roles = new List<Role>() { superAdminRole, adminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "�s�W", CatalogOrder = 1, Permission = "Catalog/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "����", CatalogOrder = 2, Permission = "Catalog/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "�ק�", CatalogOrder = 3, Permission = "Catalog/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "�R��", CatalogOrder = 4, Permission = "Catalog/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //�u�@��x�޲z
                catalogLevel01 = new Catalog { CatalogName = "�u�@��x�޲z", CatalogOrder = 1, Permission = "Diary", IsMenu = true, Comment = "", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, adminRole, generalRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "�s�W", CatalogOrder = 1, Permission = "Diary/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole, generalRole } },
                    new Catalog { CatalogName = "����", CatalogOrder = 2, Permission = "Diary/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole, generalRole } },
                    new Catalog { CatalogName = "�ק�", CatalogOrder = 3, Permission = "Diary/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole , generalRole } },
                    new Catalog { CatalogName = "�R��", CatalogOrder = 4, Permission = "Diary/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole , generalRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //�ϥΪ̺޲z
                catalogLevel01 = new Catalog { CatalogName = "�ϥΪ̺޲z", CatalogOrder = 1, Permission = "User", IsMenu = true, Comment = "", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, adminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "�s�W", CatalogOrder = 1, Permission = "User/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "����", CatalogOrder = 2, Permission = "User/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "�ק�", CatalogOrder = 3, Permission = "User/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "�R��", CatalogOrder = 4, Permission = "User/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //����޲z
                catalogLevel01 = new Catalog { CatalogName = "����޲z", CatalogOrder = 1, Permission = "Role/Index", IsMenu = true, Comment = "", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, adminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "�s�W", CatalogOrder = 1, Permission = "Role/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "����", CatalogOrder = 2, Permission = "Role/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "�ק�", CatalogOrder = 3, Permission = "Role/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "�R��", CatalogOrder = 4, Permission = "Role/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                    new Catalog { CatalogName = "����ؿ��v��", CatalogOrder = 5, Permission = "Role/EditCatalog,EditCatalogPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, adminRole } },
                }.ForEach(o => db.Catalogs.Add(o));
                #endregion

                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
