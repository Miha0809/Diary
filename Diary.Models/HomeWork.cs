using System;
using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class Homework
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Text)] public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)] public string LongDescription { get; set; }
        [DataType(DataType.Text)] public string TextToHomework { get; set; }
        [DataType(DataType.Upload)] public string PathHomework { get; set; }

        [DataType(DataType.DateTime)] public virtual DateTime StartDateTime { get; set; } = DateTime.Now;
        [DataType(DataType.DateTime)] public virtual DateTime StopDateTime { get; set; }
        public virtual Group Group { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
