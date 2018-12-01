using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace WebCore.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChangeLanguage(string langCode)
        {
            CultureInfo.CurrentCulture = new CultureInfo(langCode);
            return Ok();
        }
    }
}