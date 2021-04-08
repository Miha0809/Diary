using System;

namespace Diary.Models
{
    public class Homework
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Lesson { get; set; }

        public virtual DateTime StartDateTime { get; set; }
        public virtual DateTime StopDateTime { get; set; }
        public virtual Group Group { get; set; }
    }
}
