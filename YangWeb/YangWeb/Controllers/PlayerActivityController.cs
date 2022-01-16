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
        static List<EnemyStatsModel> allEnemyStats = new List<EnemyStatsModel>();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlayerGame()
        {
            if (allActionButtons.Count <= 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    allActionButtons.Add(new ActionButtonModel
                    {
                        ButtonAction = ran.Next(2),
                        ButtonState = false
                    });
                }
            }
           
            if(allEnemyStats.Count <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    allEnemyStats.Add(new EnemyStatsModel { 
                        EnemyName = "Monster "+i,
                        Health = 250*i,
                        Damage = 55*i
                    });
                }
            }
  
            UserDataService userData = new UserDataService();
            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();

            return View(mpass);
        }

        public IActionResult ClickButtonAction(int buttonNum)
        {
            UserDataService userData = new UserDataService();
            PlayerActivityServices playerActivity = new PlayerActivityServices();
            
            for (int i = 0; i < allActionButtons.Count; i++)
            {
                if (buttonNum == i)
                {
                    allActionButtons[i].ButtonState = true;
                    int num = ran.Next(3);
                    allActionButtons[i].Action = RandomActionMessage(num, userData.GetPlayerStats(), allEnemyStats[0]);
                    RandomAction(num, playerActivity, userData.GetPlayerStats());
                }
            }

            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();

            System.Diagnostics.Debug.WriteLine(buttonNum);
            return View("PlayerGame", mpass);
        }

        public string RandomActionMessage(int num, PlayerStatModel playerStat, EnemyStatsModel enemyStats)
        {
            switch(num)
            {
                case 0:
                    return "You Hit Enemy and Damaged him for" + playerStat.Damage;
                        break;
                case 1: case 2:
                    return "Enemy Hit You and Damaged you for 55";
                        break;
                default:
                    return "No Action";
                        break;
            }
        }

        public void RandomAction(int num, PlayerActivityServices player, PlayerStatModel playerStats)
        {
            switch (num)
            {
                case 0:
                    //player.()
                    break;
                case 1:
                    //
                    break;
            }
        }
    }
}
