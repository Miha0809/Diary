using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class LogIn
    {
        [Required(ErrorMessage = "Empty Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Empty Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Empty Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
