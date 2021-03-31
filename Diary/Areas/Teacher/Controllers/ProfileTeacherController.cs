using Microsoft.AspNetCore.Mvc;
using Diary.Services;
using System.Linq;
using Diary.Models;

namespace Diary.Controllers
{
    [Area("Teacher")]
    public class ProfileTeacherController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ProfileTeacherController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View(_diaryDbContext.Students.ToList());
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
            
            if (allStudentEmail == null &&
                allStudentPhone == null &&
                student.Password.Length >= 6)
            {
                _diaryDbContext.Students.Add(new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = student.Password
                    // Group = student.Group
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Students", "Home");
            }

            return NotFound("Error Data");
        }

        [HttpGet]
        public IActionResult RemoveStudent(int? id)
        {
            if (id == null)
            {
                return NotFound("Id student == null");
            }

            var removeStudent = _diaryDbContext.Students.Find(id);

            _diaryDbContext.Students.Remove(removeStudent);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Students", "ProfileTeacher");
        }
    }
}