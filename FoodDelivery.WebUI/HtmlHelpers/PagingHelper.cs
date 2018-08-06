using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDelivery.Web.UI.Models;
using System.Text;

namespace FoodDelivery.Web.UI.HtmlHelpes
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("Selected");
                    tag.AddCssClass("btn-success");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        //public static MvcHtmlString PageLinks(this HtmlHelper html,
        //                                     PagingInfo pagingInfo,
        //                                     Func<int, string> pageUrl)
        //{
        //    StringBuilder result = new StringBuilder();
        //    for (int i = 1; i <= pagingInfo.TotalPages; i++)
        //    {
        //        TagBuilder tag = new TagBuilder("a");
        //        tag.MergeAttribute("href", pageUrl(i));
        //        tag.InnerHtml = i.ToString();
        //        if (i == pagingInfo.CurrentPage)
        //        {
        //            tag.AddCssClass("Selected");
        //            tag.AddCssClass("btn btn-next-page");
        //        }
        //        tag.AddCssClass("btn btn-next-page");
        //        result.Append(tag.ToString());
        //    }
        //    return MvcHtmlString.Create(result.ToString());
        //}
    }
}