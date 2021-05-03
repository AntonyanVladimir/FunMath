using System;
using System.Collections.Generic;
using System.Linq;

namespace FunMath.Models
{
    public class Level
    {
        public int Id { get; set; }
        public int LevelNumber { get; set; }
        public List<Challenge> Challenges { get; set; }

    }
}
