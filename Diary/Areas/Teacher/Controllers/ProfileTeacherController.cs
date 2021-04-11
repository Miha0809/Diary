using System.Collections.Generic;
using Diary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diary.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class ProfileTeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Test(Group group)
        {
            return View(group.ToString());
        }
    }
}