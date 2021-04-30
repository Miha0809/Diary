﻿using System.Collections.Generic;
using Diary.Models.interfaces;

namespace Diary.Models
{
    public class Teacher : IUser
    {
        public int Id { get; set; }
        public int Age { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        // public virtual ICollection<Lesson> Lessons { get; set; }
    }
}