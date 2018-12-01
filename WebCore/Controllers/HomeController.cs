using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using WebCore.Models;
using WebCore.Services.Share.AppMenus;
using WebCore.Utils.Attributes;
using WebCore.Utils.Config;

namespace WebCore.Controllers
{
    public class HomeController : WebCoreBaseController
    {
        private readonly IAppMenuService appMenuService;

        public HomeController(IAppMenuService appMenuService)
        {
            this.appMenuService = appMenuService;
        }

        public IActionResult Index()
        {
            var menus = appMenuService.GetAllMenuByPermission(GetAllPermissions().ToList());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
