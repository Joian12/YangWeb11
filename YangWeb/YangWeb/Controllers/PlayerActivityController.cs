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
      
        static List<ActionButtonModel> allActionButtons = new List<ActionButtonModel>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Restart()
        {
            return View();
        }

        public IActionResult PlayerGame()
        {
            if (allActionButtons.Count <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    allActionButtons.Add(new ActionButtonModel
                    {
                        ButtonAction = ran.Next(2),
                        ButtonState = false
                    });
                }
            }

            UserDataService userData = new UserDataService();     

            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();

            return View(mpass);
        }

        public IActionResult CheckAllScores()
        {
            UserDataService user = new UserDataService();
            List<UserModel> userList = new List<UserModel>();
            List<PlayerStatModel> statList = new List<PlayerStatModel>();
            List<MultiplePassModel> passList = new List<MultiplePassModel>();

            userList = user.GetUserName().ToList();
            statList = user.GetData().ToList();


            for (int i = 0; i < userList.Count; i++)
            {
                passList.Add(new MultiplePassModel { 
                    UserModel = userList[i],
                    PlayerStat = statList[i]
                });
            }

            return View("CheckAllScores", passList);
        }

        public ActionResult LogoutReset()
        {
            RestartButtons();
            return RedirectToAction("LogOut", "Logout");
        }

        public IActionResult ClickButtonAction(int buttonNum)
        {
            PlayerStatModel newPlayer = new PlayerStatModel();
            UserDataService user = new UserDataService();
            newPlayer.Score = user.GetPlayerStats().Score;

            for (int i = 0; i < allActionButtons.Count; i++)
            {
                if (buttonNum == i && allActionButtons[i].ButtonState == false)
                {
                    allActionButtons[i].ButtonState = true;
                    int num = ran.Next(3);
                    switch (num)
                    {
                        case 0:
                            newPlayer.Score += 50;
                            allActionButtons[i].Action = "You Have Clicked 50 points";
                            user.SetPlayerStats(newPlayer);
                            break;
                        case 2:case 1:
                            allActionButtons[i].Action = "Try Again ";
                            break;
                    }
                }
            }
            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = newPlayer;

            if (!CheckIfAllButtonsAreUsed())
                return View("PlayerGame", mpass);
            else
                return View("EndPage", "Game Over");
        }

        int addNUm;
       
        public bool CheckIfAllButtonsAreUsed()
        {
            for (int i = 0; i < allActionButtons.Count; i++)
            {
                if (allActionButtons[i].ButtonState)
                    addNUm += (i-(i-1));
            }
            System.Diagnostics.Debug.WriteLine(addNUm);
            return addNUm == 20 ? true : false;
            
        }

        public List<ActionButtonModel> RestartButtons()
        {
            for (int i = 0; i < allActionButtons.Count; i++)
            {
                allActionButtons[i].ButtonState = false;
            }
            return allActionButtons;
        }
    }
}
