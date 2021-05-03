using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Models
{
    public class Challenge 
    {
        public int Id { get; set; }
        public string ChallengeText { get; set; }
        public string Solution { get; set; }
        public Level Level { get; set; }
    }
}
