using System.Collections.Generic;
using Diary.Models.interfaces;

namespace Diary.Models
{
    public class Student : IUser
    {
        public int Id { get; set; }
        public int Age { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}