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
                .First(group => thisHomework.Group.Id == group.Id);
            ViewBag.Lesson = this._diaryDbContext.Lessons.Select(lesson => lesson)
                .First(lesson => thisHomework.Lesson.Id == lesson.Id);
            ViewBag.Homework = this._diaryDbContext.ReadyHomeworks.Select(readyHomeworks => readyHomeworks)
                .First(readyHomeworks => thisHomework == readyHomeworks);
            ViewBag.Student = this._diaryDbContext.Students.Select(student => student)
                .First(student => thisHomework.Student.Email.Equals(student.Email));

            return View(_diaryDbContext.ReadyHomeworks.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(ReadyHomework readyHomework)
        {
            if (readyHomework == null)
            {
                return NotFound("readyHomework == null");
            }

            var assessment = this._diaryDbContext.Assessments
                .First(assessment => assessment.Id == readyHomework.Assessment.Id);
            var group = this._diaryDbContext.Groups
                .First(group => group.Id == readyHomework.Group.Id);
            var lesson = this._diaryDbContext.Lessons
                .First(lesson => lesson.Id == readyHomework.Lesson.Id);
            var homework = this._diaryDbContext.Homeworks
                .First(homework => homework.Id == readyHomework.Homework.Id);
            var student = this._diaryDbContext.Students
                .First(student => student.Id == readyHomework.Student.Id);

            readyHomework.Assessment = assessment;
            readyHomework.Group = group;
            readyHomework.Lesson = lesson;
            readyHomework.Homework = homework;
            readyHomework.Student = student;

            var update = new ReadyHomework()
            {
                ShortDescription = readyHomework.ShortDescription,
                LongDescription = readyHomework.LongDescription,
                TextToHomework = readyHomework.TextToHomework,
                PathHomework = readyHomework.PathHomework,
                StartDateTime = readyHomework.StartDateTime,
                StopDateTime = readyHomework.StopDateTime,
                DeliveryDateTime = readyHomework.DeliveryDateTime,
                Assessment = assessment,
                Group = group,
                Lesson = lesson,
                Homework = homework,
                Student = student
            };

            this._diaryDbContext.ReadyHomeworks.Remove(readyHomework);
            this._diaryDbContext.ReadyHomeworks.Update(update);
            this._diaryDbContext.SaveChanges();

            return RedirectToAction("ReadyHomeworks", "ReadyHomework");
        }
    }
}
