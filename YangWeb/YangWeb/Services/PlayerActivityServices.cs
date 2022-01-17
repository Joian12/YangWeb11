using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangWeb.Models;

namespace YangWeb.Services
{
    public class PlayerActivityServices
    {
        public int YouWon(EnemyStatsModel enemy)
        {
            return enemy.Health <= 0 ? enemy.GivenScore : 0;
        }

        public int CheckLevel(PlayerStatModel playerStat)
        {
            return playerStat.CurrentExperience >= playerStat.MaxExperience ? 1 : 0;
        }

        public float CheckCurrentExperience(EnemyStatsModel enemy)
        {
            return enemy.Health <= 0 ? enemy.GivenExperience : 0f;
        }

        public float CheckMaxExperience(PlayerStatModel playerStat)
        {
            return playerStat.Level*playerStat.MaxExperience;
        }

        public int CheckScore(EnemyStatsModel enemy)
        {
            return enemy.Health <= 0 ? enemy.GivenScore : 0;
        }

        public float CheckArmor(PlayerStatModel player)
        {
            return player.Armor + (player.Level / 10);
        }

        public float CheckDamage(PlayerStatModel player)
        {
            return player.Damage * (player.Level / 10);
        }

        public float DamagePlayerHealth(float damage)
        {
            return damage;
        }

        public float DamageEnemyHealth(float enemyhealth, float playerDamage)
        {
            return enemyhealth - playerDamage;
        }

        public string EnemyDefeat(float enemyHealth)
        {
            string message = enemyHealth <= 0 ? "Enemy Died" : "Enemy Surrenders";
            return message;
        }

        public string PlayerDefeat(float playerHealth)
        {
            string message = playerHealth <= 0 ? "Player Died" : "Player Surrender";
            return message;
        }
    }
}
