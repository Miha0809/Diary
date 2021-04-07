using Microsoft.AspNetCore.Mvc;
using Diary.Services;
using System.Linq;
using Diary.Models;
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View(_diaryDbContext.Students.ToList());
        }

        public IActionResult Homeworks()
        {
            return View(_diaryDbContext.Homeworks.ToList());
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
                !string.IsNullOrWhiteSpace(student.FirstName) &&
                !string.IsNullOrWhiteSpace(student.LastName) &&
                !string.IsNullOrWhiteSpace(student.Age.ToString())
                )
            {
                _diaryDbContext.Students.Add(new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = CreateRandom.Password,
                    Group = student.Group
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Students", "ProfileTeacher");
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

        [HttpGet]
        public IActionResult AddHomework()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddHomework(Homework homework)
        {

            return View();
        }
    }
}