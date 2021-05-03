using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class Student
    {
        [Key] public int Id { get; set; }
        [DataType(DataType.Text)] public ushort Age { get; set; }
        [DataType(DataType.Text)] public ushort Rating { get; set; }

        [DataType(DataType.Text)] public string FirstName { get; set; }
        [DataType(DataType.Text)] public string LastName { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        [DataType(DataType.PhoneNumber)] public string Phone { get; set; }
        [DataType(DataType.Password)] public string Password { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<ReadyHomework> ReadyHomeworks { get; set; }
    }
}