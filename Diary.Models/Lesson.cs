using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;

namespace Diary.Models
{
    public class Lesson
    {
        [Key] public int Id { get; set; }
        
        [DataType(DataType.Text)] public string Name { get; set; }
    }
}
