using System.Collections.Generic;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;
        
        public StudentController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _diaryDbContext.Students.ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudents(int id)
        {
            if (id == null)
            {
                return null;
            }

            return _diaryDbContext.Students.Find(id);
        }
    }
}