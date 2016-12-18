using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TM.Domain;
using TM.Domain.Models;

namespace TM.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //using (TMDbContext db = new TMDbContext())
            //    db.Database.Initialize(true);

        }

        //撰寫權限驗證前執行的動作
        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            IPrincipal contextUser = Context.User;

            if (contextUser.Identity.AuthenticationType == "Forms")
            {
                //取出登入使用的 FormsAuthenticationTicket  資料
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                //將於FormsAuthenticationTicket 中的使用者資料取出，並分割成陣列
                string[] roles = ticket.UserData.Split(new char[] { ',' });
                //指派角色到目前這個HttpContext 的User 物件去
                HttpContext.Current.User = new GenericPrincipal(User.Identity, roles);
                Thread.CurrentPrincipal = HttpContext.Current.User;

            }
        }
    }
}
