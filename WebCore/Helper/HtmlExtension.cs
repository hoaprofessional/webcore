using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.SystemConfigs;

namespace WebCore.Helper
{
    public static class HtmlExtension
    {
        public static string Lang(this IHtmlHelper helper, string key)
        {
            var languageService = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ILanguageProviderService>();
            return languageService.GetlangByKey(key);
        }
        public static string LangFor<TSource>(this IHtmlHelper helper, Expression<Func<TSource, object>> selector, string groupName = "")
        {
            var memberExpression = (MemberExpression)selector.Body;
            var name = memberExpression.Member.Name;
            if(String.IsNullOrWhiteSpace(groupName))
            {
                groupName = typeof(TSource).Name;
            }
            return helper.Lang($"LBL_{groupName}_{name}".ToUpper());
        }
        public static string GetSysConfigString(this IHtmlHelper helper, string key)
        {
            var systemConfigService = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ISystemConfigService>();
            return systemConfigService.GetValueString(key);
        }
        public static decimal GetSysConfigNumber(this IHtmlHelper helper, string key)
        {
            var systemConfigService = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ISystemConfigService>();
            return systemConfigService.GetValueNumber(key);
        }
        public static bool HasPermission(this IHtmlHelper helper,  string permission)
        {
            var httpContext = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            return httpContext.HttpContext.User.Claims.Any(x => x.Value == permission);
        }

        public static bool IsLogin(this IHtmlHelper helper)
        {
            IHttpContextAccessor httpContextAccessor = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
            var name = httpContextAccessor.HttpContext.User?.Identity?.Name;
            return name != null;
        }
    }
}
