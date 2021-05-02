using System.Linq;
using Diary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Student.Controllers
{
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            _diaryDbContext.ReadyHomeworks.Remove(_diaryDbContext.ReadyHomeworks.Find(id));
            return RedirectToAction("ReadyHomeworks");
        }
    }
}
