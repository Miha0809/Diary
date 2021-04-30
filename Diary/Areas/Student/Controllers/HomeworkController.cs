using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class HomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public HomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Homeworks(int? id)
        {
            /*if (id == null)
            {
                return NotFound("id == null");
            }*/

            var student = _diaryDbContext.Students.First(s => s.Email == User.Identity.Name);
            var group = _diaryDbContext.Homeworks.Select(h => h).Where(h => h.Group.Id == student.Group.Id);
            /*var homework = _diaryDbContext.Homeworks.Select(h => h).Where(h => h.Lesson.Id == id);*/

            return View(group.OrderBy(x => x.StopDateTime).ToList());
        }

        [HttpGet]
        public IActionResult HomeworkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound("id homework for details == null");
            }

            return View(_diaryDbContext.Homeworks.Find(id));
        }

        [HttpPost]
        public IActionResult HomeworkDetails(Homework homework)
        {

            var h = _diaryDbContext.Homeworks.Select(h => h).First(h => h.Id == homework.Id);

            // TODO: Зберегти групу та урок цього ДЗ
            homework.Group = h.Group;
            homework.Lesson = h.Lesson;

            _diaryDbContext.Homeworks.Update(homework);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Homeworks", "Homework");
        }

        public IActionResult Lessons()
        {
            return View(_diaryDbContext.Lessons.ToList());
        }
    }
}
