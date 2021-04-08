using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Controllers
{
    [Authorize]
    [Area("Student")]
    public class ProfileStudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
