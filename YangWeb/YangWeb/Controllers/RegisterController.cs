using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Services;
using YangWeb.Models;

namespace YangWeb.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel user)
        {
            UserDataService userData = new UserDataService();
            userData.RegisterUser(user);
            return RedirectToAction("Login", "Login");
        }
    }
}
