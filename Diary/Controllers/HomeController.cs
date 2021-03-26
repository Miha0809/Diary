using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Diary.Controllers
{
    public class HomeController : Controller
    {
        private DiaryDbContext _diaryDbContext;

        public HomeController(DiaryDbContext diaryDbContext)
        {
            _diaryDbContext = diaryDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            return View(_diaryDbContext.Students.ToList());
        }

        public IActionResult Teachers()
        {
            return View(_diaryDbContext.Teachers.ToList());
        }

        public IActionResult Groups()
        {
            return View(_diaryDbContext.Groups.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
