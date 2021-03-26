using Diary.Models;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services
{
    public class DiaryDbContext : DbContext
    {
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
    }
}
