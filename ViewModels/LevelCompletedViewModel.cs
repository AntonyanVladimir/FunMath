using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.ViewModels
{
    public class LevelCompletedViewModel
    {
        public int ReachedPoints { get; set; }
        public int PossiblePoints { get; set; }
        public int NextLevelNr { get; set; }
    }
}
