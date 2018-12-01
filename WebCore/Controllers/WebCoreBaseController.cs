using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Utils.Config;

namespace WebCore.Controllers
{
    public class WebCoreBaseController : Controller
    {
        public string[] GetAllPermissions()
        {
            if (HttpContext.User == null)
            {
                return new string[0];
            }
            return HttpContext.User.Claims.Where(x => x.Type == ConstantConfig.ClaimType.Permission).Select(x => x.Value).ToArray();
        }

        public bool HasPermission(string permission)
        {
            return HttpContext.User.Claims.Any(x => x.Value == permission);
        }
    }
}