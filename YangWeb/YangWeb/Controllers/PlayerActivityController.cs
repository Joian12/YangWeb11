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
                        GivenExperience = 35*i
                        GivenScore = 200*i
                    });
                }
            }*/
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 1", Health = 250, Damage = 55, isAlive = true, GivenExperience = 65, GivenScore = 100 });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 2", Health = 250, Damage = 55, isAlive = true, GivenExperience = 55, GivenScore = 80 });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 3", Health = 250, Damage = 55, isAlive = true, GivenExperience = 45, GivenScore = 60 });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 4", Health = 150, Damage = 55, isAlive = true, GivenExperience = 35, GivenScore = 40 });
            allEnemyStats.Add(new EnemyStatsModel { EnemyName = "Monster 5", Health = 100, Damage = 55, isAlive = true, GivenExperience = 25, GivenScore = 20 });

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
                    RandomAction(num, playerActivity, userData.GetPlayerStats(), GetMonster(allEnemyStats), userData);

                }
            }

            MultiplePassModel mpass = new MultiplePassModel();
            mpass.AllAB = allActionButtons;
            mpass.PlayerStat = userData.GetPlayerStats();

            if (userData.GetPlayerStats().Health <= 0)
                return View("YouLose");
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
            switch (num)
            {
                case 0:
                    
                    playerStats.Health = activity.DamageEnemyHealth(enemy.Health, playerStats.Damage);

                    break;
                case 1:
                    enemy.Health = activity.DamagePlayerHealth(playerStats.Health, enemy.Damage, playerStats.Armor);
                    playerStats.Level = activity.CheckLevel(playerStats);
                    playerStats.MaxExperience = activity.CheckMaxExperience(playerStats);
                    playerStats.CurrentExperience = activity.CheckCurrentExperience(enemy);
                    playerStats.Score = activity.CheckScore(enemy);
                    playerStats.Armor = activity.CheckArmor(playerStats);
                    playerStats.Damage = activity.CheckDamage(playerStats);
                    break;
            }

            userData.SetPlayerStats(playerStats);
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
