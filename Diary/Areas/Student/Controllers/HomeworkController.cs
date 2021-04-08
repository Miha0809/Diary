using Diary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;
        
        public IActionResult Homeworks()
        {
            return View();
        }
    }
}
