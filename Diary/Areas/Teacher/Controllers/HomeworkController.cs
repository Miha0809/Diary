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

        private Group _group;
        private Lesson _lesson;

        public HomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public  IActionResult Homeworks()
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
                return NotFound("homework == null");
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            // TODO: урок, до якого відноситься ця ДЗ
            // TODO: група, до якого відноситься ця ДЗ

            return View(_diaryDbContext.Homeworks.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Homework homework)
        {
            if (homework == null)
            {
                return NotFound("homework == null");
            }

            // TODO: зберегти групу, до якого відноситься ця ДЗ
            // TODO: зберегти урок, до якого відноситься ця ДЗ

            var h = _diaryDbContext.Homeworks.Select(h => h).First(h => h.Id == homework.Id);
            
            homework.Group = h.Group;
            homework.Lesson = h.Lesson;

            _diaryDbContext.Homeworks.Update(homework);
            _diaryDbContext.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            _diaryDbContext.Homeworks.Remove(_diaryDbContext.Homeworks.Find(id));
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Homeworks", "Homework");
        }
    }
}
