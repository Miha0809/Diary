using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Diary.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class ProfileTeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}