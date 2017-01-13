using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TM.Domain.Models;

namespace System.Web.Mvc
{
    public static class CommonHelper
    {
        public static MvcHtmlString DropDownYears<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            string id = Html.NameExtensions.IdFor(html, expression).ToString();
            string idForFunctionName = id.Replace('-', '_'); //注意
            string value = Html.ValueExtensions.ValueFor(html, expression).ToString();
            int searchYear = 0;

            if(!string.IsNullOrWhiteSpace(value))
            {
                searchYear = int.Parse(value);
            }

            MvcHtmlString result = Html.SelectExtensions.DropDownListFor(html, expression, GetYears(searchYear), new { @class = "form-control" });

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString DropDownForUserId<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            TMDbContext TMDb = new TMDbContext();
            string id = Html.NameExtensions.IdFor(html, expression).ToString();
            string idForFunctionName = id.Replace('-', '_'); //注意
            string value = Html.ValueExtensions.ValueFor(html, expression).ToString();
            List<SelectListItem> usersSLItems = new List<SelectListItem>();
            List<User> users = TMDb.Users.AsNoTracking().OrderBy(x => x.EmployeeId).ToList();

            foreach(var user in users)
            {
                usersSLItems.Add(new SelectListItem { Text = user.EmployeeId + " " + user.UserName,Value = user.UserId.ToString()});
            }

            MvcHtmlString result = Html.SelectExtensions.DropDownListFor(html, expression, usersSLItems, new { @class = "form-control" });

            return MvcHtmlString.Create(result.ToString());
        }

        private static List<SelectListItem> GetYears(int year,int startYear = 2016)
        {
            List<SelectListItem> years = new List<SelectListItem>();
            DateTime nowDate = System.DateTime.Now;
            int nowYear = nowDate.Year;

            for (int i = startYear; i <= nowYear; i++)
            {
                years.Add(new SelectListItem { Text = i.ToString() + " 年", Value = i.ToString(), Selected = (i == year) });
            }

            return years;
        }
    }
}