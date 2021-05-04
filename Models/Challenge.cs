using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Models
{
    public class Challenge 
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Level))]
        public int LevelId{ get; set; }
        public string ChallengeText { get; set; }
        public string Solution { get; set; }
        public Level Level { get; set; }
        public int LevelNumber { get; set; }
    }
}
