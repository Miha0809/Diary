using System.Collections.Generic;

namespace Diary.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
