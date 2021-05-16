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
            var myHomeworks = _diaryDbContext.ReadyHomeworks.Select(x => x)
                .Where(x => x.Student.Email.Equals(User.Identity.Name)).ToList();

            if (myHomeworks.Count == 0)
            {
                return NotFound("You not have ready homeworks!");
            }

            return View(myHomeworks);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            return View(this._diaryDbContext.ReadyHomeworks.Find(id));
        }
    }
}
