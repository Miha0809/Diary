using System;
using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class ReadyHomework
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Text)] public ushort Assessment { get; set; }

        [DataType(DataType.Text)] public string ShortDescription { get; set; }
        [DataType(DataType.MultilineText)] public string LongDescription { get; set; }
        [DataType(DataType.MultilineText)] public string TextToHomework { get; set; }
        [DataType(DataType.Text)] public string PathHomework { get; set; }

        [DataType(DataType.DateTime)] public virtual DateTime StartDateTime { get; set; }
        [DataType(DataType.DateTime)] public virtual DateTime StopDateTime { get; set; }
        [DataType(DataType.DateTime)] public virtual DateTime DeliveryDateTime { get; set; }
        public virtual Group Group { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Homework Homework { get; set; }
        public virtual Student Student { get; set; }
    }
}