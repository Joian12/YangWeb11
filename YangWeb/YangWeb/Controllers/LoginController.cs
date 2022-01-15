using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Services;
using YangWeb.Models;

namespace YangWeb.Controllers
{
    public class LoginController : Controller
    {   
        public IActionResult Index(UserModel user)
        {
            UserDataService tryService = new UserDataService();
            return View();
        }

        public IActionResult SuccessLogin(UserModel user)
        {
            UserSession.SetUserSession(user.Username);
            System.Diagnostics.Debug.WriteLine(UserSession.GetUserSession());
            return View("Views/Home/Index.cshtml");
        }
    }
}
