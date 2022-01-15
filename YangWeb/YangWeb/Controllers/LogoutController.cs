using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Services;

namespace YangWeb.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Home/Index.cshtml");
        }

        public IActionResult LogOut()
        {
            UserSession.LogoutSession();
            return View("Views/Home/Index.cshtml");
        }
    }
}
