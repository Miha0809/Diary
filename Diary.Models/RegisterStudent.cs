using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class RegisterStudent
    {
        [Key] public int Id { get; set; }
        
        [DataType(DataType.Text)] public int Age { get; set; }

        [DataType(DataType.Text)] public string FirstName { get; set; }
        [DataType(DataType.Text)] public string LastName { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        [DataType(DataType.PhoneNumber)] public string Phone { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }
    }
}
