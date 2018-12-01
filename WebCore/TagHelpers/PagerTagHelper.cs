using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Net;
using WebCore.Utils.ModelHelper;

namespace WebCore.TagHelpers
{
    [HtmlTargetElement("custompager", TagStructure = TagStructure.WithoutEndTag)]
    public class PagerTagHelper : TagHelper
    {
        private readonly HttpContext _httpContext;
        private readonly IUrlHelper _urlHelper;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PagerTagHelper(IHttpContextAccessor accessor, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _httpContext = accessor.HttpContext;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        [HtmlAttributeName("pager-model")]
        public PagingResultBase Model { get; set; }

        [HtmlAttributeName("pager-link")]
        public string ActionLink { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Model == null)
            {
                return;
            }

            if (Model.PageCount == 0)
            {
                return;
            }
            string action = ViewContext.RouteData.Values["action"].ToString();
            string urlTemplate = WebUtility.UrlDecode(_urlHelper.Action(action, new { page = "{0}" }));

            string requestKeys = "";

            if (ActionLink != null)
            {
                string[] links = ActionLink.Split('?');

                urlTemplate = WebUtility.UrlDecode(links[0] + "?page={0}");
                if (links.Count() == 2)
                {
                    requestKeys = "&" + links[1].ToLower();
                }
            }
            HttpRequest request = _httpContext.Request;
            foreach (string key in request.Query.Keys)
            {
                if (key == "page")
                {
                    continue;
                }

                if (requestKeys.IndexOf($"&{key.ToLower()}=") >= 0)
                {
                    continue;
                }

                urlTemplate += "&" + key + "=" + request.Query[key];
            }

            if (requestKeys != "")
            {
                urlTemplate += requestKeys;
            }


            int startIndex = Math.Max(Model.CurrentPage - 5, 1);
            int finishIndex = Math.Min(Model.CurrentPage + 5, Model.PageCount);

            output.TagName = "";
            output.Content.AppendHtml("<ul class=\"pagination\">");
            AddPageLink(output, string.Format(urlTemplate, 1), "&laquo;");

            for (int i = startIndex; i <= finishIndex; i++)
            {
                if (i == Model.CurrentPage)
                {
                    AddCurrentPageLink(output, i);
                }
                else
                {
                    AddPageLink(output, string.Format(urlTemplate, i), i.ToString());
                }
            }

            AddPageLink(output, string.Format(urlTemplate, Model.PageCount), "&raquo;");
            output.Content.AppendHtml("</ul>");
        }

        private void AddPageLink(TagHelperOutput output, string url, string text)
        {
            output.Content.AppendHtml("<li><a href=\"");
            output.Content.AppendHtml(url);
            output.Content.AppendHtml("\">");
            output.Content.AppendHtml(text);
            output.Content.AppendHtml("</a>");
            output.Content.AppendHtml("</li>");
        }

        private void AddCurrentPageLink(TagHelperOutput output, int page)
        {
            output.Content.AppendHtml("<li class=\"active\">");
            output.Content.AppendHtml("<span>");
            output.Content.AppendHtml(page.ToString());
            output.Content.AppendHtml("</span>");
            output.Content.AppendHtml("</li>");
        }
    }
}
