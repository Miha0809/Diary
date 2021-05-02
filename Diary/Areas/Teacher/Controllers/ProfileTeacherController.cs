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
            return View();
        }

        [HttpGet]
        public IActionResult Accout()
        {
            var teacher = _diaryDbContext.Teachers.FirstOrDefault(x => x.Email.Equals(User.Identity.Name));

            if (teacher == null)
            {
                return NotFound("teacher == null");
            }

            return View(teacher);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            ViewBag.Groups = new SelectList(_diaryDbContext.Groups.ToList(), "Id", "Name");
            ViewBag.Lessons = new SelectList(_diaryDbContext.Lessons.ToList(), "Id", "Name");

            return View(_diaryDbContext.Teachers.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Teacher teacher)
        {
            if (teacher == null)
            {
                return NotFound("teacher == null");
            }

            _diaryDbContext.Teachers.Update(teacher);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Accout", "ProfileTeacher");
        }
    }
}