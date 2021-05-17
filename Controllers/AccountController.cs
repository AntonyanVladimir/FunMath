
using FunMath.Data;
using FunMath.Interfaces;
using FunMath.Models;
using FunMath.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FunMath.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly TaskContext _taskContext;

        public AccountController(TaskContext tasContext, ITokenService tokenService)
        {
            _taskContext = tasContext;
            _tokenService = tokenService;
           
        }
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Register([FromForm] RegisterViewModel model)
        {
            if (await UserExists(model.Username))
                return BadRequest("Der Benutzer schon existiert.");

            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                Username = model.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key,
                Age = model.Age,
                Role = "Admin"
                
            };
            if (string.IsNullOrEmpty(user.Role))
                user.Role = "Player";

            _taskContext.AppUsers.Add(user);
            await _taskContext.SaveChangesAsync();
            //return new UserViewModel() 
            //{
            //    Username = user.Username,
            //    Token = _tokenService.CreateToken(user)
            //};
            
            return RedirectToAction("Startseite", "Home");
        }
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Login([FromForm] LoginViewModel loginViewModel)
        {
            var user = await _taskContext.AppUsers.SingleOrDefaultAsync(m => m.Username == loginViewModel.Username);

            if (user == null)
                return Unauthorized("Invalid User");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginViewModel.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }

            return RedirectToAction("Startseite", "Home");
        }
        private async Task<bool> UserExists(string username)
        {
            return await _taskContext.AppUsers.AnyAsync(m => m.Username == username.ToLower());
        }
    }
}
