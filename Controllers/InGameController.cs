using FunMath.Data;
using FunMath.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class InGameController : Controller
    {
        readonly TaskContext _context;
        public InGameController(TaskContext context)
        {
            _context = context;
        }
        public IActionResult LoadLevel(int levelId, int challengeNummer)
        {

            Level level = _context.Levels.Include(m => m.Challenges)
                        .FirstOrDefault(m => m.LevelNumber == challengeNummer);
            var challenges = level.Challenges;
            var currentChallenge = challenges[challengeNummer-1];

            return View(currentChallenge);
        }
    }
}
