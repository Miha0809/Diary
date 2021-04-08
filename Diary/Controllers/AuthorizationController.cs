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
    public class AuthorizationController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public AuthorizationController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IActionResult LogInAsTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInAsTeacher(LogIn logIn)
        {
            var allTeachers = _diaryDbContext.Teachers.FirstOrDefault(x => x.Email == logIn.Email && x.Password == logIn.Password);

            if (allTeachers == null)
            {
                return NotFound("allTeachers is null");
            }

            Authentication(logIn.Email);

            return RedirectToAction("Index", "ProfileTeacher", new
            {
                area = "Teacher"
            });
        }

        [HttpGet]
        public IActionResult LogInAsStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInAsStudent(LogIn logIn)
        {
            var allStudents = _diaryDbContext.Students.FirstOrDefault(x => x.Email == logIn.Email && x.Password == logIn.Password);

            if (allStudents == null)
            {
                return NotFound("allStudents is null");
            }

            Authentication(logIn.Email);

            return RedirectToAction("Student", "ProfileStudent", new
            {
                area = "Student"
            });
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
    }
}