using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Diary.Models;
using Diary.Services;

namespace Diary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DiaryDbContext _diaryDbContext;

        public WeatherForecastController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            /*var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Student()
            {
                FirstName = "Mark",
                LastName = "Full",
                Age = rng.Next(18, 30),
                Email = $"{rng.Next(000000001, int.MaxValue)}@gmail.com"
            })
            .ToArray();*/

            return _diaryDbContext.Students.ToList();
        }
    }
}
