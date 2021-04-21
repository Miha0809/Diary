using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Add()
        {
            ViewBag.Groups = new SelectList(_diaryDbContext.Groups.ToList(), "Id", "Name");
            ViewBag.Lessons = new SelectList(_diaryDbContext.Lessons.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Homework homework)
        {
            if (homework == null)
            {
                return NotFound("homrwork == null");
            }

            if (!string.IsNullOrWhiteSpace(homework.ShortDescription) &&
                !string.IsNullOrWhiteSpace(homework.LongDescription) &&
                homework.Group != null &&
                homework.Lesson != null)
            {
                var group = _diaryDbContext.Groups.FirstOrDefault(x => x.Id == homework.Group.Id);
                var lesson = _diaryDbContext.Lessons.FirstOrDefault(x => x.Id == homework.Lesson.Id);

                _diaryDbContext.Homeworks.Add(new Homework()
                {
                    ShortDescription = homework.ShortDescription,
                    LongDescription = homework.LongDescription,
                    StartDateTime = homework.StartDateTime,
                    StopDateTime = homework.StopDateTime,
                    Group = group,
                    Lesson = lesson
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Homeworks", "Homework");
            }

            return View();
        }
    }
}
