using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Diary.Controllers
{
    public class AccountController : Controller
    {
        private DiaryDbContext _diaryDbContext;

        public AccountController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInTeacher logIn)
        {
            var allTeacher = _diaryDbContext.Teachers.FirstOrDefault(x => x.Email == logIn.Email && x.Password == logIn.Password);

            if (allTeacher == null)
            {
                return NotFound();
            }

            Authentication(logIn.Email);

            return RedirectToAction("Account");
        }

        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void Authentication(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult Account()
        {
            return View();
        }
    }
}
