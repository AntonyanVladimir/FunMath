﻿using FunMath.Data;
using FunMath.Models;
using FunMath.Services;
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
        private readonly TaskContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InGameController(TaskContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var httpContext = _httpContextAccessor.HttpContext;
            var claimsPrincipal = httpContext.User;
            _context = context;
        }
        public IActionResult CreateFirstLevel()
        {
            var firstLevel = CreateLevelWithChallenges();
            _context.Levels.Add(firstLevel);

            return RedirectToAction(nameof(LoadLevel), new { levelNumber = firstLevel.LevelNumber });
        }
        public IActionResult LoadLevel(int levelNumber, int challengeIndex, int Points)
        {
            if (_context.Levels.Count() == 0)
            {
                var firstLevel = CreateLevelWithChallenges();
                _context.Levels.Add(firstLevel);
            }

            Level level = _context.Levels.Include(m => m.Challenges)
                        .FirstOrDefault(m => m.LevelNumber == levelNumber);
            if (level == null)
            {
                level = CreateLevelWithChallenges();
            }
            var challenges = level.Challenges;

            if (challenges.Count > challengeIndex)
            {
                Challenge currentChallenge = challenges[challengeIndex];

                var vm = new CurrentChallengeViewModel()
                {
                    ChallengeText = currentChallenge.ChallengeText,
                    LevelNumber = currentChallenge.LevelNumber,
                    Solution = currentChallenge.Solution,
                    Index = challengeIndex,
                    LevelChallengesCount = level.Challenges.Count,
                    Punkte = Points,
                };

                return View(vm);
            }
            else
            {
                var vm = new LevelCompletedViewModel()
                {
                    NextLevelNr = levelNumber + 1,
                    PossiblePoints = challenges.Count,
                    ReachedPoints = Points,
                };
                return RedirectToAction(nameof(LevelCompleted), vm);
            }
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
                return RedirectToAction(nameof(LoadLevel), new { levelNumber = currentChallenge.LevelNumber, challengeIndex = ++currentChallenge.Index, Points = ++currentChallenge.Punkte });
            }

            return RedirectToAction(nameof(WrongAnswer), currentChallenge);
        }
        public IActionResult LevelCompleted(LevelCompletedViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return View(viewModel);
        }
        public IActionResult WrongAnswer(CurrentChallengeViewModel currentChallenge)
        {
            return View(currentChallenge);
        }
        public IActionResult GenerateNewLevel()
        {
            CreateLevelWithChallenges();

            return RedirectToAction("ShowTasks", "ManageData", new { Area = "Admin"});
        }

        private Level CreateLevelWithChallenges()
        {
            //count new Level Number
            var newLevelNr = _context.Levels.Count() + 1;
            //generate new Level with Challenges
            var challengeGen = new ChallengeGenerator();
            var newLevel = challengeGen.GenerateChallengesAndLevel(newLevelNr);

            //add newLevel 
            _context.Levels.Add(newLevel);

            _context.SaveChanges();

            return newLevel;
        }
    }
}
