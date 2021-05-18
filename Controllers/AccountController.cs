
using FunMath.Data;
using FunMath.Interfaces;
using FunMath.Models;
using FunMath.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace FunMath.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TaskContext _taskContext;
        readonly SignInManager<AppUser> _signInManager;


        public AccountController(TaskContext tasContext, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _taskContext = tasContext;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            var userManager = _httpContextAccessor.HttpContext.RequestServices.GetService<AspNetUserManager<AppUser>>();
            _signInManager = _httpContextAccessor.HttpContext.RequestServices.GetService<SignInManager<AppUser>>();

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

            //var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
           
            //await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
            //    new ClaimsPrincipal(identity));

            return RedirectToAction("Startseite", "Home");
        }
        private async Task<bool> UserExists(string username)
        {
            return await _taskContext.AppUsers.AnyAsync(m => m.Username == username.ToLower());
        }
    }
}
