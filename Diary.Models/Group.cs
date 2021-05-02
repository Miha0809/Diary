using System.ComponentModel.DataAnnotations;

namespace Diary.Models
{
    public class Group
    {
        [Key] public int Id { get; set; }
        [DataType(DataType.Text)] public string Name { get; set; }
    }
}
