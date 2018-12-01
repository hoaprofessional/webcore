using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : AdminBaseController
    {
        public DashboardController(IServiceProvider serviceProvider):
            base(serviceProvider)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}