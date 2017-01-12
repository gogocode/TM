using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using TM.Domain.Models;

namespace System.Web.Mvc
{
    public static class ItemHelper
    {
        public static MvcHtmlString DropDownItem<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            TMDbContext _TMDb = new TMDbContext();
            string id = Html.NameExtensions.IdFor(html, expression).ToString();
            string idForFunctionName = id.Replace('-', '_'); //注意
            string value = Html.ValueExtensions.ValueFor(html, expression).ToString();
            var input = Html.InputExtensions.TextBoxFor(html, expression, new { @class = "form-control" });

            List<Item> items = _TMDb.Items.OrderBy(x => x.ItemId).ToList();

            var retorno = new StringBuilder();

            retorno.Append("<div class='row'>");
            retorno.Append("<div class='col-lg-10'>");
            retorno.Append("<div class='input-group'>");
            retorno.Append("<div class='input-group-btn'>");
            retorno.Append("<button type='button' class='btn btn-default dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>項目<span class='caret'></span></button>");
            retorno.Append("<ul class='dropdown-menu'>");

            foreach(var item in items)
            {
                retorno.Append(string.Format( "<li><a class='ItemBtn' data-id='{0}'>{0}</a></li>",item.ItemName));
            }

            retorno.Append("</ul>");
            retorno.Append("</div>");
            //retorno.Append("<input type='text' class='form-control' aria-label='...' value=''>");
            retorno.Append(input);
            retorno.Append("</div>");
            retorno.Append("</div>");
            retorno.Append("</div>");

            return MvcHtmlString.Create(retorno.ToString());
        }
    }
}