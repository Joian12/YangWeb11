using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangWeb.Models
{
    public class PlayerStatModel
    {
        public int Level { get; set; }
        public float MaxExperience { get; set; }
        public float CurrentExperience {get; set;}
        public float Health { get; set; }
        public int Score { get; set; }
        public float Armor { get; set; }
        public float Damage { get; set; }
    }
}
