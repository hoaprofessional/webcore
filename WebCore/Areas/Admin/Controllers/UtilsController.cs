﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Areas.Admin.Controllers
{
    public class UtilsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}