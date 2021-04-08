using System.Collections.Generic;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Teacher.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class StudentController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public StudentController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Students()
        {
            return View(_diaryDbContext.Students.ToList());
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            var student = new Models.Student();
            (Models.Student student, List<Group> group) all = (student, _diaryDbContext.Groups.ToList());
            return View(all);
        }

        [HttpPost]
        public IActionResult AddStudent(Models.Student student)
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
                _diaryDbContext.Students.Add(new Models.Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = CreateRandom.Password(),
                    Group = student.Group
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Students", "Student");
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

            return RedirectToAction("Students", "Student");
        }
    }
}
