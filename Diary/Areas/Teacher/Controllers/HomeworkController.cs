using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;

namespace Diary.Areas.Teacher.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class HomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public HomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Homeworks()
        {
            return View(_diaryDbContext.Homeworks.ToList());
        }

        [HttpGet]
        public IActionResult AddHomework()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHomework(Homework homework)
        {

            return View();
        }
    }
}
