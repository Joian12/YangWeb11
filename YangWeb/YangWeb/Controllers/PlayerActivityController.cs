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

            /*if(allEnemyStats.Count <= 0)
            {
                for (int i = 10; i > 0; i--)
                {
                    allEnemyStats.Add(new EnemyStatsModel { 
                        EnemyName = "Monster "+i,
                        Health = 250*i,
                        Damage = 55*i,
                        isAlive = true,
                        isAvailable = false
                    });
                }
            }*/
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 1", Health = 250, Damage = 55, isAlive = true, isAvailable = false});
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 2", Health = 250, Damage = 55, isAlive = true, isAvailable = false });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 3", Health = 250, Damage = 55, isAlive = true, isAvailable = false });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 4", Health = 0, Damage = 55, isAlive = true, isAvailable = false });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 5", Health = 0, Damage = 55, isAlive = true, isAvailable = false });

            for (int i = 0; i < allEnemyStats.Count; i++)
            {
                if(allEnemyStats[i].Health > 0 )
                    System.Diagnostics.Debug.WriteLine(allEnemyStats[i].EnemyName);
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
                if (buttonNum == i && allActionButtons[i].ButtonState == false)
                {
                    allActionButtons[i].ButtonState = true;
                    int num = ran.Next(3);
                   
                    allActionButtons[i].Action = RandomActionMessage(num, userData.GetPlayerStats(), GetMonster(allEnemyStats));
                    RandomAction(num, playerActivity, userData.GetPlayerStats());
                }
            }

            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();

        
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

        public EnemyStatsModel GetMonster(List<EnemyStatsModel> allEnemy)
        {
            for (int i = 0; i < allEnemy.Count; i++)
            {
                if (allEnemy[i].Health > 0)
                    return allEnemy[i];
            }
            return null;
        }
    }
}
