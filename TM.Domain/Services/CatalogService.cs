using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using System.Data.Entity;

namespace TM.Domain.Services
{
    public class CatalogService
    {
        private TMDbContext _db;

        public CatalogService()
        {
            _db = new TMDbContext();
        }

        public Catalog Find(int id)
        {
            return _db.Catalogs.Include(c => c.ParentCatalog).Where(x=>x.CatalogId == id).FirstOrDefault();
        }

        public List<Catalog> FindAll()
        {
            return _db.Catalogs.Include(c => c.ParentCatalog).OrderBy(x=>x.CatalogId).ToList();
        }

        public int IsValidPermissionByRoles(string account, string controllerName, string actionName)
        {
            int cnt = 0;
            User user = _db.Users.Where(x => x.Account == account).FirstOrDefault();
            List<Role> roles = user.Roles.ToList();

            //檢查該user所擁有的roles的permission是否吻合controllerName/actionName
            //permission的樣子:Catalog/Create,CreatePost
            foreach (Role role in roles)
            {
                foreach(Catalog catalog in role.Catalogs)
                {
                    string[] permission = catalog.Permission.Split('/');
                    if (permission.Count() <= 1)
                    {
                        continue;
                    }
                    
                    if((permission[0] == controllerName))
                    {
                        string[] actions = permission[1].Split(',');
                        for (int i = 0; i < actions.Count(); i++)
                        {
                            if (actions[i] == actionName)
                            {
                                cnt++;
                            }
                        }
                    } 
                }
            }

            return cnt;
        }

        public int ModifyRoleCatalogs(int roleId, List<int> selectedCatalogIds)
        {
            try
            {
                Role role = _db.Roles.Where(x => x.RoleId == roleId).FirstOrDefault();
                List<Catalog> delCatalogs = role.Catalogs.ToList();

                List<Catalog> catalogs = _db.Catalogs.Where(x => selectedCatalogIds.Contains(x.CatalogId)).ToList();

                foreach(var delCatalog in delCatalogs)
                {
                    role.Catalogs.Remove(delCatalog);
                }

                role.Catalogs = catalogs;
                return _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int Create(Catalog catalog)
        {
            try
            {
                _db.Catalogs.Add(catalog);
                return _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int Modify(Catalog catalog)
        {
            try
            {
                _db.Entry(catalog).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
               List<Catalog> catalogs =  _db.Catalogs.Include(c => c.ParentCatalog).OrderBy(x => x.CatalogId).ToList();

                Catalog catalog = catalogs.Where(x => x.CatalogId == id).FirstOrDefault();
                RecursiveDelte(catalog);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RecursiveDelte(Catalog catalog)
        {
            foreach(var data in catalog.SubCatalogs.ToArray())
            {
                RecursiveDelte(data);
            }

            _db.Catalogs.Remove(catalog);
        }
    }
}
