using DBModel;
using DBModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebCalc
{
    public static class ViewExtensions
    {
        [Obsolete("Not use", true)]
        public static MvcHtmlString GetFavoriteLinks(this HtmlHelper html)
        {
            var sb = new StringBuilder();

            var favorites = DB.GetFavorites("");

            foreach (var item in favorites)
            {
                sb.AppendLine($"<li><a href=\"/Calc/Index?operName={item.Name}\">{item.Name}</a></li>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static IEnumerable<Favorite> GetFavorites(this HtmlHelper html)
        {
            var username = html.ViewContext.HttpContext.User.Identity.Name;
            return DB.GetFavorites(username);
        }
    }
}