using FunMath.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.ViewModels
{
    public class LevelsViewModel
    {
        public IEnumerable<SelectListItem> Levels { get; private set; }
        public LevelsViewModel(List<Level> levels)
        {
            Levels = levels.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.LevelNumber.ToString(),
                                      Value = x.LevelNumber.ToString(),
                                  });
        }
    }
}
