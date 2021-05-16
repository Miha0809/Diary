using System.Linq;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            return View(_diaryDbContext.Students.OrderByDescending(x => x.Rating).ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Groups = new SelectList(_diaryDbContext.Groups.ToList(), "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(Models.Student student)
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
                var group = _diaryDbContext.Groups.FirstOrDefault(x => x.Id == student.Group.Id);

                _diaryDbContext.Students.Add(new Models.Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Email = student.Email,
                    Phone = student.Phone,
                    Password = CreateRandom.Password(),
                    Group = group
                });
                _diaryDbContext.SaveChanges();

                return RedirectToAction("Students", "Student");
            }

            return NotFound("Error Data");
        }

        [HttpGet]
        public IActionResult Remove(int? id)
        {
            if (id == null)
            {
                return NotFound("Id student == null");
            }

            _diaryDbContext.Students.Remove(_diaryDbContext.Students.Find(id));
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Students", "Student");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            var student = this._diaryDbContext.Students
                .First(student => student.Id == id);
            
            ViewBag.Groups = new SelectList(_diaryDbContext.Groups.ToList(), "Id", "Name");
            ViewBag.Password = student.Password;

            return View(_diaryDbContext.Students.Find(id));
        }
        
        [HttpPost]
        public IActionResult Edit(Models.Student student)
        {
            if (student == null)
            {
                return NotFound("Student == null");
            }

            var st = this._diaryDbContext.Students
                .First(st => st.Email.Equals(student.Email));
            var group = this._diaryDbContext.Groups
                .First(group => group.Id == student.Group.Id);
            var homeworks = this._diaryDbContext.Homeworks.Select(x => x)
                .Where(homework => homework.Group.Id == group.Id).ToList();
            var readyHomeworks = this._diaryDbContext.ReadyHomeworks.Select(x => x)
                .Where(readyHomework => readyHomework.Group.Id == group.Id).ToList();

            student.Group = group;
            student.Homeworks = homeworks;
            student.ReadyHomeworks = readyHomeworks;

            var update = new Models.Student()
            {
                Age = student.Age,
                Rating = student.Rating,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Phone = student.Phone,
                Password = student.Password,
                Group = group,
                Homeworks = homeworks,
                ReadyHomeworks = readyHomeworks
            };

            this._diaryDbContext.Students.Remove(student);
            _diaryDbContext.Students.Update(update);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Students", "Student");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            return View(_diaryDbContext.Students.Find(id));
        }
    }
}
