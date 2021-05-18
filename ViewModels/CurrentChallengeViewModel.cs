using FunMath.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.ViewModels
{
    public class CurrentChallengeViewModel
    {
        public string ChallengeText { get; set; }
        public string Solution { get; set; }
        public int LevelNumber { get; set; }
        public int Index { get; set; }
        public string UserAntwort { get; set; }
    }
}
