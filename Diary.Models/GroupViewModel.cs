using System.Collections.Generic;
using System.Web.Mvc;

namespace Diary.Models
{
    public class GroupViewModel
    {
        public string Name { get; set; }
        public List<SelectListItem> Group { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "IT11", Text = Groups.IT11.ToString() },
            new SelectListItem { Value = "IT12", Text = Groups.IT12.ToString() }
        };
    }
}
