﻿namespace Diary.Models
{
    public class Homework
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Lesson { get; set; }
        public string ImagePath { get; set; }

        public virtual Group Group { get; set; }
    }
}
