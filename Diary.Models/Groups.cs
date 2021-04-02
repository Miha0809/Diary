using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public enum Groups
    {
        [Display(Name = "IT-11")] IT11,
        [Display(Name = "IT-12")] IT12,
        [Display(Name = "IT-21")] IT21,
        [Display(Name = "IT-22")] IT22,
        [Display(Name = "IT-31")] IT31,
        [Display(Name = "IT-32")] IT32,
        [Display(Name = "IT-41")] IT41,
        [Display(Name = "IT-42")] IT42,
        [Display(Name = "IC-11")] IC11,
        [Display(Name = "IC-12")] IC12,
        [Display(Name = "IC-21")] IC21,
        [Display(Name = "IC-22")] IC22,
        [Display(Name = "IC-31")] IC31,
        [Display(Name = "IC-32")] IC32,
        [Display(Name = "IC-41")] IC41,
        [Display(Name = "IC-42")] IC42
    }
}