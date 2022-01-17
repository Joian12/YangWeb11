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
        static EnemyStatsModel currentEnemy;
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
                for (int i = 0; i < 20; i++)
                {
                    allActionButtons.Add(new ActionButtonModel
                    {
                        ButtonAction = ran.Next(2),
                        ButtonState = false
                    });
                }
            }

 
            ResetEnemyList();
            
            currentEnemy = GetMonster(allEnemyStats);

            UserDataService userData = new UserDataService();

            ResetAfterDeath(userData);

            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();
            mpass.EnemyStats = currentEnemy;

            

            return View(mpass);
        }

        public ActionResult LogoutReset()
        {
            ResetEnemyList();
            currentEnemy = null;
            System.Diagnostics.Debug.WriteLine("Reset After Logout");
            RestartButtons();
            return RedirectToAction("LogOut", "Logout");
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
                    int num = ran.Next(2);
                    
                    allActionButtons[i].Action = RandomActionMessage(num, userData.GetPlayerStats(), currentEnemy);
                    RandomAction(num, playerActivity, userData.GetPlayerStats(), currentEnemy, userData);
                    
                }
            }
            System.Diagnostics.Debug.WriteLine(currentEnemy.EnemyName + "  "+ currentEnemy.Health);
            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();
            mpass.EnemyStats = currentEnemy;

            if (userData.GetPlayerStats().Health <= 0 || currentEnemy.Health <= 0)
            {
                string message = currentEnemy.Health <= 0 ? "You Won The Battle" : "You Lost The Battle";
                RestartButtons();
                return View("EndPage", message);
            }
            else
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
                    return "Enemy Hit You and Damaged you for "+ enemyStats.Damage;
                        break;
                default:
                    return "No Action";
                        break;
            }
        }

        public void RandomAction(int num, PlayerActivityServices activity, PlayerStatModel playerStats, EnemyStatsModel enemy, UserDataService userData)
        {
            
            PlayerStatModel player = new PlayerStatModel();
            switch (num)
            {
                case 0: case 2:
                     enemy.Health = activity.DamageEnemyHealth(enemy.Health, playerStats.Damage);
                     player.Health = playerStats.Health;
                     player.Level = playerStats.Level;
                     player.MaxExperience = playerStats.MaxExperience;
                     player.CurrentExperience = playerStats.CurrentExperience;
                     player.Score = playerStats.Score;
                     player.Armor = playerStats.Armor;
                     player.Damage = playerStats.Damage;
                     userData.SetPlayerStats(player);
                    break;
                case 1:                    
                    player.Health = playerStats.Health - activity.DamagePlayerHealth( enemy.Damage);
                    player.Level = playerStats.Level + activity.CheckLevel(playerStats);
                    player.MaxExperience = playerStats.MaxExperience + activity.CheckMaxExperience(playerStats);
                    player.CurrentExperience = playerStats.CurrentExperience + activity.CheckCurrentExperience(enemy);
                    player.Score = playerStats.Score + activity.CheckScore(enemy);
                    player.Armor = playerStats.Armor + activity.CheckArmor(playerStats);
                    player.Damage = playerStats.Damage + activity.CheckDamage(playerStats);
                    userData.SetPlayerStats(player);
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

        public bool CheckIfAllButtonsAreUsed()
        {
            int num = 0;
            for (int i = 0; i < allActionButtons.Count; i++)
            {
                if (allActionButtons[i].ButtonState)
                    num += i;
            }
            return num == 15 ? true : false;
        }

        public void RestartButtons()
        {
            for (int i = 0; i < allActionButtons.Count; i++)
            {
                allActionButtons[i].ButtonState = false;
            }
        }

        public void ResetEnemyList()
        {
           if (allEnemyStats.Count <= 0)
           {
               for (int i = 0; i < 10; i++)
               {
                   allEnemyStats.Add(new EnemyStatsModel
                   {
                       EnemyName = "Monster " + i,
                       Health = 250 * i,
                       Damage = 55 * i,
                       isAlive = true,
                       GivenExperience = 35 * i,
                       GivenScore = 200 * i
                   });
               }
           }

            for (int i = 0; i < allEnemyStats.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(allEnemyStats[i].EnemyName+"  "+allEnemyStats[i].Damage);
            }
        }

        public void ResetAfterDeath(UserDataService userData)
        {
            PlayerStatModel newPlay = new PlayerStatModel(); 
            newPlay.Level = 1;
            newPlay.MaxExperience = 150;
            newPlay.CurrentExperience = 0;
            newPlay.Health = 250;
            newPlay.Armor = 1;
            newPlay.Score = 0;
            newPlay.Damage = 55;


            userData.SetPlayerStats(newPlay);
        }
    }
}
