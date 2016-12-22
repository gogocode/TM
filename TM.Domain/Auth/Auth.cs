using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Models;
using TM.Domain.Services;

namespace TM.Domain
{
    public static class Auth
    {
        private static TMDbContext _db = new TMDbContext();
    
        public static bool IsShow(string permission)
        {
            string[] permissions = permission.Split('/');
            string controllerName = permissions[0];
            string actionName = permissions[1];

            bool isShow = IsValidPermissionByRoles(LoginState.LoginAccount, controllerName, actionName) > 0;

            return isShow;
        }

        public static int IsValidPermissionByRoles(string account, string controllerName, string actionName)
        {
            int cnt = 0;
            User user = _db.Users.AsNoTracking().Where(x => x.Account == account).FirstOrDefault();
            List<Role> roles = user.Roles.ToList();

            //檢查該user所擁有的roles的permission是否吻合controllerName/actionName
            //permission的樣子:Catalog/Create,CreatePost
            foreach (Role role in roles)
            {
                foreach (Catalog catalog in role.Catalogs)
                {
                    string[] permission = catalog.Permission.Split('/');
                    if (permission.Count() <= 1)
                    {
                        continue;
                    }

                    if ((permission[0] == controllerName))
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
    }
}
