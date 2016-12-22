using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TM.Domain;
using TM.Domain.Models;
using TM.Domain.Services;

namespace TM.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckAuthAttribute : AuthorizeAttribute
    {
        private CatalogService _CatalogService;
        private UserService _UserService;

        public CheckAuthAttribute()
        {
            _CatalogService = new CatalogService();
            _UserService = new UserService();
        }

        /// <summary>
        /// The name of each action that must be permissible for this method, separated by a comma.
        /// </summary>
        public string Permissions { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            string controllerName = routeData.GetRequiredString("controller");
            string actionName = routeData.GetRequiredString("action");

            if(!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            bool isValid = Auth.IsValidPermissionByRoles(httpContext.User.Identity.Name, controllerName, actionName) > 0;

            if(LoginState.LoginUserId == 0)
            {
                User user = _UserService.FindUserByAccount(httpContext.User.Identity.Name);

                LoginState.LoginAccount = user.Account;
                LoginState.LoginUserId = user.UserId;
                LoginState.LoginUserName = user.UserName;
            }

            return isValid;
        }
    }
}