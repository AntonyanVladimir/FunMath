using FunMath.Data;
using FunMath.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class UsersController : Controller
    {
        readonly TaskContext _taskContext;
        public UsersController(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _taskContext.AppUsers.ToListAsync();
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _taskContext.AppUsers.FindAsync(id);
        }

    }
}
