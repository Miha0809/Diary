using System.Linq;
using Diary.Models;
using Diary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Areas.Teacher.Controllers
{
    [Authorize]
    [Area("Teacher")]
    public class GroupController : Controller
    {
        private readonly DiaryDbContext _diaryDbContext;

        public GroupController(DiaryDbContext diaryDbContext)
        {
            this._diaryDbContext = diaryDbContext;
        }

        public IActionResult Groups()
        {
            return View(_diaryDbContext.Groups.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group group)
        {
            if (group == null)
            {
                return NotFound("group == null");
            }

            _diaryDbContext.Groups.Add(new Group()
            {
                Name = group.Name
            });
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Groups", "Group");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            return View(_diaryDbContext.Groups.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Group group)
        {
            if (group == null)
            {
                return NotFound("group == null");
            }

            _diaryDbContext.Groups.Update(group);
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Groups", "Group");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            return View(_diaryDbContext.Groups.Find(id));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("id == null");
            }

            _diaryDbContext.Groups.Remove(_diaryDbContext.Groups.Find(id));
            _diaryDbContext.SaveChanges();

            return RedirectToAction("Groups", "Group");
        }
    }
}
