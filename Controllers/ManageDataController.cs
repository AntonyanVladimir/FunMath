using FunMath.Data;
using FunMath.Models;
using FunMath.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class ManageDataController : Controller
    {
        private readonly TaskContext _context;
        private readonly List<Level> _levels;
        public ManageDataController(TaskContext context)
        {
            _context = context;
            _levels = context.Levels.ToList();
        }
        public IActionResult AddTask()
        {
            var viewModel = new LevelsViewModel(_levels);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveTask(Challenge challenge)
        {
            if (challenge is null)
            {
                throw new ArgumentNullException(nameof(challenge));
            }
            var level = _context.Levels.FirstOrDefault(m => m.LevelNumber == challenge.LevelNumber);
            if (level == null)
                return BadRequest("Kann nicht hinzugefügt werden.Level existiert nicht.");

            level.Challenges.Add(challenge);
            _context.SaveChanges();

            return RedirectToAction(nameof(AddTask));
        }
        [HttpGet]
        public IActionResult ShowTasks()
        {
            var tasks = _context.Levels.ToList();

            return View(tasks);
        }
        public IActionResult DeleteLevelWithChallenges(int id)
        {
            var level = _context.Levels.FirstOrDefault(m => m.Id == id);
            if (level == null)
                return BadRequest("Der Level wurde schon gelöscht.");
            _context.Levels.Remove(level);
            _context.SaveChanges();

            return RedirectToAction(nameof(ShowTasks));
        }
        public IActionResult ShowLevelChallenges(int id)
        {
            Level level = _context.Levels.Include(m=>m.Challenges)
                        .FirstOrDefault(m => m.Id == id);
            if(level == null)
                return BadRequest("Level fehlt.");

            return View(level);
        }
        public IActionResult EditTask(int id)
        {
            var task = _context.Challenges.FirstOrDefault(m => m.Id == id);

            if (task == null)
                return BadRequest($"Task mit der id {id} kann nicht gefunden werden.");
            var viewModel = new EditChallengeViewModel();
            viewModel.Id = task.Id;
            viewModel.ChallengeText = task.ChallengeText;
            viewModel.Solution = task.Solution;
            viewModel.LevelNumber = task.LevelNumber;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SaveEditedTask(EditChallengeViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            var changedLevel = _context.Levels.FirstOrDefault(m => m.LevelNumber == viewModel.LevelNumber);
            var challangeFromDb = _context.Challenges.FirstOrDefault(m => m.Id == viewModel.Id);
            challangeFromDb.Id = viewModel.Id;
            challangeFromDb.LevelId = changedLevel.Id;
            challangeFromDb.ChallengeText = viewModel.ChallengeText;
            challangeFromDb.Solution = viewModel.Solution;

            _context.SaveChanges();

            return RedirectToAction(nameof(ShowTasks));
        }

    }
}
