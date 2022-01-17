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
            return View();
        }

        public IActionResult Login(UserModel user)
        {
            UserDataService userData = new UserDataService();
            if (userData.CheckLogin(user))
            {
                UserSession.SetUserSession(user.Username);
                return View("Views/Home/Index.cshtml");
            }
            else
                return View("Index");
            
        }
    }
}
