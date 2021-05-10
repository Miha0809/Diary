using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diary.Areas.Teacher.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class ReadyHomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ReadyHomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult ReadyHomeworks()
        {
            return View(this._diaryDbContext.ReadyHomeworks.ToList());
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            var thisHomework = this._diaryDbContext.ReadyHomeworks.First(homework => homework.Id == id);

            ViewBag.Assessment = new SelectList(this._diaryDbContext.Assessments.ToList(), "Id", "Mark");
            ViewBag.Group = this._diaryDbContext.Groups.Select(group => group)
                .Where(group => thisHomework.Group.Id == group.Id);
            ViewBag.Lesson = this._diaryDbContext.Lessons.Select(lesson => lesson)
                .Where(lesson => thisHomework.Lesson.Id == lesson.Id);
            ViewBag.Homework = this._diaryDbContext.ReadyHomeworks.Select(readyHomeworks => readyHomeworks)
                .Where(readyHomeworks => thisHomework == readyHomeworks);
            ViewBag.Student = this._diaryDbContext.Students.Select(student => student)
                .Where(student => thisHomework.Student.Id == student.Id);

            return View(_diaryDbContext.ReadyHomeworks.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(ReadyHomework readyHomework)
        {
            if (readyHomework == null)
            {
                return NotFound("readyHomework == null");
            }

            var update = new ReadyHomework()
            {
                ShortDescription = readyHomework.ShortDescription,
                LongDescription = readyHomework.LongDescription,
                TextToHomework = readyHomework.TextToHomework,
                PathHomework = readyHomework.PathHomework,
                StartDateTime = readyHomework.StartDateTime,
                StopDateTime = readyHomework.StopDateTime,
                DeliveryDateTime = readyHomework.DeliveryDateTime,
                Assessment = readyHomework.Assessment,
                Group = readyHomework.Group,
                Lesson = readyHomework.Lesson,
                Homework = readyHomework.Homework,
                Student = readyHomework.Student
            };

            this._diaryDbContext.ReadyHomeworks.Update(update);
            this._diaryDbContext.SaveChanges();

            return RedirectToAction("ReadyHomeworks", "ReadyHomework");
        }
    }
}
