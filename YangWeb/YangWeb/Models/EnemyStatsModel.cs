using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangWeb.Models
{
    public class EnemyStatsModel
    {   
        public string EnemyName { get; set; }
        public int Health { get; set; }
        public float Damage { get; set; }
        public bool isAlive { get; set; }
        public bool isAvailable { get; set;}
    }
}
