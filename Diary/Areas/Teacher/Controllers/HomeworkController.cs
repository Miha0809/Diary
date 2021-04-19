using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;

namespace Diary.Areas.Teacher.Controllers
{
    /*[Authorize]*/
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Homework homework)
        {
            if (homework == null)
            {
                return NotFound("homrwork == null");
            }

            var addHomework = new Homework()
            {
                ShortDescription = homework.ShortDescription,
                LongDescription = homework.LongDescription,
                Lesson = homework.Lesson,
                StartDateTime = homework.StartDateTime,
                StopDateTime = homework.StopDateTime,
                Group = homework.Group
            };
            _diaryDbContext.SaveChanges();

            return View();
        }
    }
}
