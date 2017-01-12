using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using TM.Domain.Models;

namespace System.Web.Mvc
{
    public static class EmployeeInfoHelper
    {
        public static MvcHtmlString DropDownEmployeeInfo<TModel, TValue>(this HtmlHelper<TModel> html, 
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            TMDbContext _TMDb = new TMDbContext();
            string id = Html.NameExtensions.IdFor(html, expression).ToString();
            string idForFunctionName = id.Replace('-', '_'); //注意
            string value = Html.ValueExtensions.ValueFor(html, expression).ToString();

            List<User> users = _TMDb.Users.OrderBy(x=>x.EmployeeId).ToList();
            
            //CurrencyType 下拉選單
            List<SelectListItem> employeeInfos = new List<SelectListItem>();
            employeeInfos.Add(new SelectListItem() { Text = "請選擇", Value = "" });

            foreach (var user in users)
            {
                employeeInfos.Add(new SelectListItem() { Text = user.EmployeeId + " " + user.UserName, Value = user.EmployeeId 
                    ,Selected = (user.EmployeeId == value) });
            }

            MvcHtmlString result = Html.SelectExtensions.DropDownListFor(html, expression, employeeInfos, new { @class = "form-control" });

            return MvcHtmlString.Create(result.ToString());
        }
    }
}