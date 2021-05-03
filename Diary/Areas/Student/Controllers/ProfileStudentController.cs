using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Controllers
{
    [Authorize]
    [Area("Student")]
    public class ProfileStudentController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ProfileStudentController(DiaryDbContext diaryDbContext)
        {
            ViewBag.Rating = this._diaryDbContext.Students.Select(x => x).First(x => x.Email.Equals(User.Identity.Name)).Rating;
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Account()
        {
            var student = _diaryDbContext.Students.FirstOrDefault(x => x.Email.Equals(User.Identity.Name));

            if (student == null)
            {
                return NotFound("student == null");
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            return View(_diaryDbContext.Students.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (student == null)
            {
                return NotFound("student == null");
            }

            _diaryDbContext.Students.Update(student);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Account", "ProfileStudent");
        }
    }
}
