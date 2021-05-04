using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.ViewModels
{
    public class EditChallengeViewModel
    {
        public int Id { get; set; }
        public string ChallengeText { get; set; }
        public string Solution { get; set; }
        public int LevelNumber { get; set; }
    }
}
