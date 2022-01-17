using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangWeb.Models
{
    public class EnemyStatsModel
    {   
        public string EnemyName { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public bool isAlive { get; set; }
        public float GivenExperience { get; set; }
        public int GivenScore { get; set; }
    }
}
