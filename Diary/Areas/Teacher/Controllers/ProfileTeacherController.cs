using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Diary.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class ProfileTeacherController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ProfileTeacherController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Index()
        {
            return View(this._diaryDbContext.Students.OrderByDescending(x => x.Rating).ToList());
        }

        [HttpGet]
        public IActionResult Accout()
        {
            var teacher = this._diaryDbContext.Teachers.FirstOrDefault(x => x.Email.Equals(User.Identity.Name));

            if (teacher == null)
            {
                return NotFound("teacher == null");
            }

            ViewBag.Password = teacher.Password;

            return View(teacher);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            var teacher = this._diaryDbContext.Teachers
                .First(teacher => teacher.Id == id);
            var lesson = this._diaryDbContext.Lessons.Select(lessons => lessons)
                .Where(lesson => lesson.Id == teacher.Lesson.Id).ToList();

            ViewBag.Groups = new SelectList(this._diaryDbContext.Groups.ToList(), "Id", "Name");
            ViewBag.Lessons = new SelectList(lesson, "Id", "Name");

            return View(this._diaryDbContext.Teachers.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Teacher teacher)
        {
            if (teacher == null)
            {
                return NotFound("teacher == null");
            }

            this._diaryDbContext.Teachers.Update(teacher);
            this._diaryDbContext.SaveChanges();

            return RedirectToAction("Accout", "ProfileTeacher");
        }
    }
}