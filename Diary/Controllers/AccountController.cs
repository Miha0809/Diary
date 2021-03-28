using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Diary.Controllers
{
    public class AccountController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public AccountController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IActionResult AccountTeacher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            ViewBag.Group = _diaryDbContext.Groups.Select(group => group.Id);
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (student == null)
            {
                return NotFound("student is null");
            }

            var allStudentEmail = _diaryDbContext.Students.FirstOrDefault(user => user.Email == student.Email);
            var allStudentPhone = _diaryDbContext.Students.FirstOrDefault(user => user.Phone == student.Phone);
            
            if (allStudentEmail == null && allStudentPhone == null)
            {
                var addStudent = _diaryDbContext.Students.Add(new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = student.Password
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Students", "Home");
            }

            return NotFound("Error Data");
        }

        /**************************************** About Student ****************************************/

        [HttpGet]
        public IActionResult AccountStudent()
        {
            return View();
        }
    }
}
