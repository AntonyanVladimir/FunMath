using FunMath.Data;
using FunMath.Models;
using FunMath.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class InGameController : Controller
    {
        readonly TaskContext _context;
        public InGameController(TaskContext context, IHttpContextAccessor httpContextAccessor)
        {
            var userName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            _context = context;
        }
        public IActionResult LoadLevel(int levelNumber, int challengeIndex)
        {

            Level level = _context.Levels.Include(m => m.Challenges)
                        .FirstOrDefault(m => m.LevelNumber == levelNumber);
            var challenges = level.Challenges;

            if (challenges.Count > challengeIndex - 1)
            {
                Challenge currentChallenge = challenges[challengeIndex - 1];

                var vm = new CurrentChallengeViewModel()
                {
                    ChallengeText = currentChallenge.ChallengeText,
                    LevelNumber = currentChallenge.LevelNumber,
                    Solution = currentChallenge.Solution,
                    Index = challengeIndex,
                    LevelChallengesCount = level.Challenges.Count

                };

                return View(vm);

            }
            return RedirectToAction(nameof(LevelCompleted));
        }
        [HttpPost]
        public IActionResult CheckAntwort([FromForm] CurrentChallengeViewModel currentChallenge)
        {
            if (currentChallenge is null)
            {
                throw new ArgumentNullException(nameof(currentChallenge));
            }

            if (currentChallenge.Solution == currentChallenge.UserAntwort)
            {
                return RedirectToAction(nameof(LoadLevel), new { levelNumber = currentChallenge.LevelNumber, challengeIndex = ++currentChallenge.Index });
            }

            return null;
        }
        public IActionResult LevelCompleted()
        {
            return View();
        }

    }
}
