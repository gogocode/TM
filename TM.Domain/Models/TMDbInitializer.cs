using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Models
{
    public class TMDbInitializer : CreateDatabaseIfNotExists<TMDbContext>
    {
        protected override void Seed(TMDbContext db)
        {
            InitDBData(db);
        }

        protected void InitDBData(TMDbContext db)
        {
            try
            {
                #region 使用者
                //使用者
                User superAdmin = new User() { UserName = "超級管理員",EmployeeId = "SuperaAmin", Account = "superadmin", Password = "81DC9BDB52D04DC20036DBD8313ED055", Email = "t@86shop.com.tw", IsActive = true };
                User tonyho = new User() { UserName = "tonyho",EmployeeId = "1698", Account = "tonyho", Password = "81DC9BDB52D04DC20036DBD8313ED055", Email = "tonyho@86shop.com.tw", IsActive = true };
                db.Users.Add(superAdmin);
                db.Users.Add(tonyho);
                #endregion

                #region 角色
                //超級管理員
                Role superAdminRole = new Role { RoleEngName = "superadmin", RoleName = "超級管理員", Users = new List<User>() { superAdmin } };
                //一般使用者
                Role generalRole = new Role { RoleEngName = "general", RoleName = "一般使用者", Users = new List<User>() { tonyho } };
                //SLOT權限管理者
                Role slotAuthRole = new Role { RoleEngName = "slotauth", RoleName = "SLOT權限管理者", Users = new List<User>() { tonyho } };

                db.Roles.Add(superAdminRole);
                db.Roles.Add(generalRole);
                db.Roles.Add(slotAuthRole);
                #endregion

                #region 項目
                new List<Item>
                {
                    new Item {ItemName = "CBDM跨境直郵" },
                    new Item {ItemName = "SLOT員購系統" },
                    new Item {ItemName = "NTMS新品系統" },
                    new Item {ItemName = "PK撿貨系統" },
                    new Item {ItemName = "TM工作管理系統" },
                    new Item {ItemName = "SLOT權限管理" },
                    new Item {ItemName = "MEETING會議" },
                    new Item {ItemName = "國定假日" },
                }.ForEach(o => db.Items.Add(o));
                #endregion

                #region 功能維護
                Catalog catalogLevel01 = new Catalog();

                //功能維護管理
                catalogLevel01 = new Catalog { CatalogName = "功能維護管理", CatalogOrder = 11, Permission = "Catalog", IsMenu = true, Comment = "", IconClass = "fa fa-tasks", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "新增", CatalogOrder = 1, Permission = "Catalog/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "功能維護首頁", CatalogOrder = 2, Permission = "Catalog/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "修改", CatalogOrder = 3, Permission = "Catalog/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "刪除", CatalogOrder = 4, Permission = "Catalog/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //工作日誌管理
                catalogLevel01 = new Catalog { CatalogName = "工作日誌管理", CatalogOrder = 20, Permission = "Diary", IsMenu = true, Comment = "", IconClass = "fa fa-file-text-o", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, generalRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "新增", CatalogOrder = 1, Permission = "Diary/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, generalRole } },
                    new Catalog { CatalogName = "工作日誌管理首頁", CatalogOrder = 2, Permission = "Diary/Index,IndexPost", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, generalRole } },
                    new Catalog { CatalogName = "修改", CatalogOrder = 3, Permission = "Diary/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole , generalRole } },
                    new Catalog { CatalogName = "刪除", CatalogOrder = 4, Permission = "Diary/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole , generalRole } },
                    new Catalog { CatalogName = "檢視使用者日誌", CatalogOrder = 5, Permission = "Diary/LookUserDiary,LookUserDiaryPost", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "項目管理", CatalogOrder = 6, Permission = "Diary/ItemIndex,ItemCreate,ItemEdit,ItemDelete", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole , generalRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //基本功能
                catalogLevel01 = new Catalog { CatalogName = "基本功能", CatalogOrder = 21, Permission = "Fundamental", IsMenu = true, Comment = "", IconClass = "fa fa-apple", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, generalRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "SLOT功能權限紀錄", CatalogOrder = 1, Permission = "SlotFuncAuthRecord/Index,IndexPost,Edit,EditPost,CreatePost,Delete", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, slotAuthRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //使用者管理
                catalogLevel01 = new Catalog { CatalogName = "使用者管理", CatalogOrder = 12, Permission = "User", IsMenu = true, Comment = "", IconClass = "fa fa-user", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "新增", CatalogOrder = 1, Permission = "User/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "使用者首頁", CatalogOrder = 2, Permission = "User/Index,IndexPost", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "修改", CatalogOrder = 3, Permission = "User/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "刪除", CatalogOrder = 4, Permission = "User/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "修改角色", CatalogOrder = 5, Permission = "User/EditRole,EditRolePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //角色管理
                catalogLevel01 = new Catalog { CatalogName = "角色管理", CatalogOrder = 10, Permission = "Role", IsMenu = true, Comment = "", IconClass = "fa fa-users", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "新增", CatalogOrder = 1, Permission = "Role/Create,CreatePost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "角色首頁", CatalogOrder = 2, Permission = "Role/Index", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "修改", CatalogOrder = 3, Permission = "Role/Edit,EditPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "刪除", CatalogOrder = 4, Permission = "Role/Delete", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                    new Catalog { CatalogName = "角色目錄權限", CatalogOrder = 5, Permission = "Role/EditCatalog,EditCatalogPost", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //匯入管理
                catalogLevel01 = new Catalog { CatalogName = "匯入管理", CatalogOrder = 30, Permission = "Export", IsMenu = true, Comment = "", IconClass = "glyphicon glyphicon-export", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "匯入工作日誌", CatalogOrder = 1, Permission = "Export/Diary,DiaryPost", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                //圖表
                catalogLevel01 = new Catalog { CatalogName = "圖表管理", CatalogOrder = 31, Permission = "Chart", IsMenu = true, Comment = "", IconClass = "fa fa-bar-chart-o", ParentCatalogId = null, Roles = new List<Role>() { superAdminRole, generalRole } };
                db.Catalogs.Add(catalogLevel01);
                new List<Catalog> {
                    new Catalog { CatalogName = "個人工作統計圖", CatalogOrder = 1, Permission = "Chart/DiaryChart,DiaryChartPost", IsMenu = true, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole, generalRole } },
                    new Catalog { CatalogName = "搜尋工作統計權限", CatalogOrder = 2, Permission = "Chart/SearchDiaryChart", IsMenu = false, Comment = "", IconClass = "", ParentCatalog = catalogLevel01, Roles = new List<Role>() { superAdminRole } },
                }.ForEach(o => db.Catalogs.Add(o));

                #endregion

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
