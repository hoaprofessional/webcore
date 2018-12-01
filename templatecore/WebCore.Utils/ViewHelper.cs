using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCore.Utils
{
    public static class ViewHelper
    {
        public static async Task<string> RenderPartialView(this ViewContext context, string viewName, ICompositeViewEngine viewEngine = null, ViewEngineResult viewResult = null)
        {
            viewEngine = viewEngine ?? context.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();

            viewResult = viewResult ?? viewEngine.FindView(context, viewName, false);

            await viewResult.View.RenderAsync(context);

            return context.Writer.ToString();

        }
    }
}
