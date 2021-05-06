using System.Collections.Generic;
using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public HomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IEnumerable<Homework> GetHomeworks()
        {
            return _diaryDbContext.Homeworks.ToList();
        }

        [HttpGet("{id}")]
        public Homework GetHomework(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return _diaryDbContext.Homeworks.Find(id);
        }
    }
}