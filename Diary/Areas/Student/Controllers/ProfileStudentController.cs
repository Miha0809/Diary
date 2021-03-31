using Diary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Controllers
{
    [Area("Student")]
    public class ProfileStudentController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ProfileStudentController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        /**************************************** About Student ****************************************/

        [HttpGet]
        public IActionResult Student()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HomeWorks()
        {
            return View();
        }
    }
}
