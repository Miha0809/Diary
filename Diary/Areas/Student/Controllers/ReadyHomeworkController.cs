using System.Linq;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
    [Authorize]
    [Area("Student")]
    public class ReadyHomeworkController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public ReadyHomeworkController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult ReadyHomeworks()
        {
            return View(_diaryDbContext.ReadyHomeworks.ToList());
        }
    }
}
