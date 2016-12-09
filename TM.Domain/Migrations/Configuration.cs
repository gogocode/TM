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

                //�ϥΪ�
                User superadmin = new User() { UserName = "�W�ź޲z��", Account = "superadmin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                User admin = new User() { UserName = "�޲z��", Account = "admin", Password = "1234", Email = "t@86shop.com.tw", IsActive = true };
                db.Users.Add(superadmin);
                db.Users.Add(admin);

                //�ؿ��v��
                Catalog catalogLevel01 = new Catalog();
                catalogLevel01 = new Catalog { CatalogName = "�ؿ����@(SuperAdmin Only)", CatalogOrder = 1, CatalogCode = "01", IsMenu = true, MenuControllerName = "Catalogs", IconClass = "fa fa-user-secret fa-fw", ParentCatalogId = null, Comment = "" };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "����",              CatalogOrder = 1, CatalogCode = "0100", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "�s�W",              CatalogOrder = 2, CatalogCode = "0101", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "�ק�",              CatalogOrder = 3, CatalogCode = "0102", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "�R��",              CatalogOrder = 4, CatalogCode = "0103", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "�����v���ק�",       CatalogOrder = 5, CatalogCode = "0104", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                    new Catalog { CatalogName = "�ϥΪ��v���ק�",     CatalogOrder = 6, CatalogCode = "0105", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01 },
                }.ForEach(o => db.Catalogs.Add(o));

                //����
                new List<Role> {
                    new Role {  RoleName = "�W�ź޲z��", Users = new List<User>() { superadmin } },
                    new Role {  RoleName = "�޲z��", Users = new List<User>() { admin } },
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
