using FunMath.Data;
using FunMath.Models;
using FunMath.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class ManageDataController : Controller
    {
        private readonly TaskContext _context;
        public ManageDataController(TaskContext context)
        {
            _context = context;
        }
        public IActionResult AddTask()
        {
            //var listOfLevels = new List<Level>()
            //{
            //    new Level()
            //    {
            //        LevelNumber = 1,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 2,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 3,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 4,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 5,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 6,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 7,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 8,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 9,
            //    },
            //    new Level()
            //    {
            //        LevelNumber = 10,
            //    }
            //};
            //_context.Levels.AddRange(listOfLevels);
            //_context.SaveChanges();
            var viewModel = new LevelsViewModel(_context.Levels.ToList());
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

    }
}
