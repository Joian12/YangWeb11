using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangWeb.Services
{
    public class PlayerActivityServices
    {
        public int YouWon(int score)
        {
            return score;
        }

        public float DamagePlayer(float playerHealth, float damage, float armor)
        {
            return playerHealth - (damage - (armor * damage));
        }

        public float DamageEnemy(float enemyhealth, float playerDamage)
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
