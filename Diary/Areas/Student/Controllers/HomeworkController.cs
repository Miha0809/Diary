using System;
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

        public HomeworkController(DiaryDbContext diaryDbContext, IWebHostEnvironment appEnvironment)
        {
            this._diaryDbContext = diaryDbContext;
            this._appEnvironment = appEnvironment;
        }

        public IActionResult Homeworks()
        {
            var student = this._diaryDbContext.Students.First(s => s.Email == User.Identity.Name);
            var group = this._diaryDbContext.Homeworks.Select(h => h).Where(h => h.Group.Id == student.Group.Id);

            return View(group.OrderBy(x => x.StopDateTime).ToList());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("id homework for details == null");
            }

            return View(this._diaryDbContext.Homeworks.Find(id));
        }

        [HttpPost]
        public IActionResult Details(Homework homework)
        {

            // TODO: null is sent to object null
            // TODO: save lesson this homework

            var student = this._diaryDbContext.Students.FirstOrDefault(s => s.Email.Equals(User.Identity.Name));
            var readyHomework = new ReadyHomework()
            {
                ShortDescription = homework.ShortDescription,
                LongDescription = homework.LongDescription,
                StartDateTime = homework.StartDateTime,
                StopDateTime = homework.StopDateTime,
                DeliveryDateTime = DateTime.Now,
                Group = student.Group,
                Lesson = homework.Lesson,
                Homework = homework,
                Student = student
            };

            if (homework == null || readyHomework == null)
            {
                return NotFound("homework == null || readyHomework == null");
            }

            var uploadedFile = HttpContext.Request.Form.Files[0];
            var path = "/Files/" + uploadedFile.FileName;

            using (var fileStream = new FileStream(this._appEnvironment.WebRootPath + path, FileMode.Create))
            {
                uploadedFile.CopyTo(fileStream);
            }

            readyHomework.PathHomework = path;
            // TODO: remove homework ready by student
            
            this._diaryDbContext.ReadyHomeworks.Add(readyHomework);
            this._diaryDbContext.SaveChanges();

            return RedirectToAction("Homeworks", "Homework");
        }
    }
}