using System.IO;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class HomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeworkController(DiaryDbContext diaryDbContext, IWebHostEnvironment  appEnvironment)
        {
            this._diaryDbContext = diaryDbContext;
            this._appEnvironment = appEnvironment;
        }

        public IActionResult Homeworks()
        {
            var student = _diaryDbContext.Students.First(s => s.Email == User.Identity.Name);
            var group = _diaryDbContext.Homeworks.Select(h => h).Where(h => h.Group.Id == student.Group.Id);

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

            // TODO: null is sent to object null
            // TODO: save lesson this homework

            var student = _diaryDbContext.Students.FirstOrDefault(s => s.Email.Equals(User.Identity.Name));
            var readyHomework = new ReadyHomework()
            {
                ShortDescription = homework.ShortDescription,
                LongDescription = homework.LongDescription,
                TextToHomework = homework.TextToHomework,
                PathHomework = homework.PathHomework,
                StartDateTime = homework.StartDateTime,
                StopDateTime = homework.StopDateTime,
                Group = student.Group,
                Lesson = homework.Lesson,
                Homework = homework,
                Student = student
            };

            if (homework == null ||
                readyHomework == null ||
                HttpContext.Request.Form.Files[0] == null)
            {
                return NotFound("homework == null || readyHomework == null || HttpContext.Request.Form.Files[0] == null");
            }

            var uploadedFile = HttpContext.Request.Form.Files[0];
            var path = "/Files/" + uploadedFile.FileName;

            using (var fileStream = new FileStream(this._appEnvironment.WebRootPath + path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }

            readyHomework.PathHomework = path;

            /*var h = _diaryDbContext.Homeworks.Select(h => h).First(h => h.Id == homework.Id);

            // TODO: Зберегти групу та урок цього ДЗ
            homework.Group = h.Group;
            homework.Lesson = h.Lesson;*/

            _diaryDbContext.Homeworks.Update(homework);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Homeworks", "Homework");
        }

        private void addReadyHomework(ReadyHomework readyHomework)
        {
            
        }

        public IActionResult Lessons()
        {
            return View(_diaryDbContext.Lessons.ToList());
        }
    }
}
