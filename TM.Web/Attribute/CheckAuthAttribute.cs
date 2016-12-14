using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TM.Domain.Services;

namespace TM.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckAuthAttribute : AuthorizeAttribute
    {
        private CatalogService _CatalogService = new CatalogService();

        /// <summary>
        /// The name of each action that must be permissible for this method, separated by a comma.
        /// </summary>
        public string Permissions { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            string controllerName = routeData.GetRequiredString("controller");
            string actionName = routeData.GetRequiredString("action");

            bool isValid = _CatalogService.IsValidPermissionByRoles(httpContext.User.Identity.Name, controllerName, actionName) > 0;

            return isValid;
        }
    }
}