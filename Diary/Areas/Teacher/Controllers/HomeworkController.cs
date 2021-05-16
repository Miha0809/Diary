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

        public  IActionResult Homeworks()
        {
            return View(_diaryDbContext.Homeworks.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            var teacher = this._diaryDbContext.Teachers.First(teacher => teacher.Email.Equals(User.Identity.Name));
            
            var lessons = this._diaryDbContext.Lessons.Select(lesson => lesson)
                .Where(lesson => lesson.Id == teacher.Lesson.Id).ToList();

            ViewBag.Groups = new SelectList(this._diaryDbContext.Groups.ToList(), "Id", "Name");
            ViewBag.Lesson = new SelectList(lessons, "Id", "Name");

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
                var group = this._diaryDbContext.Groups
                    .First(group => group.Id == homework.Group.Id);
                var lesson = this._diaryDbContext.Lessons
                    .First(lesson => lesson.Id == homework.Lesson.Id);

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

            return View(_diaryDbContext.Homeworks.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Homework homework)
        {
            if (homework == null)
            {
                return NotFound("homework == null");
            }

            var group = this._diaryDbContext.Groups
                .First(group => group.Id == homework.Group.Id);
            var lesson = this._diaryDbContext.Lessons
                .First(lesson => lesson.Id == homework.Lesson.Id);

            homework.Group = group;
            homework.Lesson = lesson;

            var update = new Homework()
            {
                ShortDescription = homework.ShortDescription,
                LongDescription = homework.LongDescription,
                StartDateTime = homework.StartDateTime,
                StopDateTime = homework.StopDateTime,
                Group = group,
                Lesson = lesson
            };

            _diaryDbContext.Homeworks.Remove(homework);
            _diaryDbContext.Homeworks.Update(update);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Homeworks", "Homework");
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
