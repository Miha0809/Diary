using System.Linq;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class LessonController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public LessonController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Lessons()
        {
            return View(_diaryDbContext.Lessons.ToList());
        }
    }
}
