using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Services;
using YangWeb.Models;

namespace YangWeb.Controllers
{
    public class PlayerActivityController : Controller
    {
        Random ran = new Random();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlayerGame()
        {
            UserDataService userData = new UserDataService();
            //System.Diagnostics.Debug.WriteLine(userData.GetPlayerStats().Health);
            
            return View(userData.GetPlayerStats());
        }

        public IActionResult ClickButtonAction(int buttonNum)
        {
            UserDataService userData = new UserDataService();
            System.Diagnostics.Debug.WriteLine(buttonNum);
            return View("PlayerGame", userData.GetPlayerStats());
        }
    }
}
