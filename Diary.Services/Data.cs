using System;
using System.Linq;
using Diary.Models;

namespace Diary.Services
{
    public class Data
    {
        public static void Initialize(DiaryDbContext diaryDbContext)
        {
            if (diaryDbContext == null)
            {
                throw new ArgumentNullException(nameof(diaryDbContext));
            }

            if (!diaryDbContext.Groups.Any())
            {

                diaryDbContext.Groups.AddRange(
                    new Group() { Name = "IT-11" },
                    new Group() { Name = "IT-12" },
                    new Group() { Name = "IC-11" },
                    new Group() { Name = "IC-12" }
                );

                diaryDbContext.SaveChanges();
            }

            if (!diaryDbContext.Students.Any())
            {
                diaryDbContext.Students.AddRange(
                    new Student()
                    {
                        Age = 17,
                        FirstName = "Mark",
                        LastName = "Full",
                        Email = "troublesomebass18@gmail.com",
                        Phone = "380963235503",
                        Password = "qwerty",
                        Group = diaryDbContext.Groups.FirstOrDefault(x => x.Id == 1)
                    },
                    new Student()
                    {
                        Age = 16,
                        FirstName = "Sviatoslav",
                        LastName = "Kuchirka",
                        Email = "5623@gmail.com",
                        Phone = "380674617583",
                        Password = CreateRandom.Password(),
                        Group = diaryDbContext.Groups.FirstOrDefault(x => x.Id == 1)
                    },
                    new Student()
                    {
                        Age = 16,
                        FirstName = "Ira",
                        LastName = "Dranovska",
                        Email = "irad@gmail.com",
                        Phone = "380676575325",
                        Password = CreateRandom.Password(),
                        Group = diaryDbContext.Groups.FirstOrDefault(x => x.Id == 1)
                    }
                );

                diaryDbContext.SaveChanges();
            }

            if (!diaryDbContext.Lessons.Any())
            {
                diaryDbContext.Lessons.AddRange(
                    new Lesson() { Name = "Math" },
                    new Lesson() { Name = "History" },
                    new Lesson() { Name = "Geography" },
                    new Lesson() { Name = "Physics" },
                    new Lesson() { Name = "Protection of Ukraine" }
                );

                diaryDbContext.SaveChanges();
            }

            if (!diaryDbContext.Teachers.Any())
            {
                diaryDbContext.Teachers.AddRange(
                    new Teacher()
                    {
                        Age = 26,
                        FirstName = "Natalia",
                        LastName = "Zubko",
                        Email = "natalia@ukr.net",
                        Phone = "380971710678",
                        Password = "qwerty",
                        Groups = diaryDbContext.Groups.ToList(),
                        Lesson = diaryDbContext.Lessons.FirstOrDefault(l => l.Id == 1)
                    }
                );

                diaryDbContext.SaveChanges();
            }

            if (!diaryDbContext.Homeworks.Any())
            {
                diaryDbContext.Homeworks.AddRange(
                    new Homework()
                    {
                        ShortDescription = "Логарифми",
                        LongDescription = "Прочитати 1пар. Впр.: 1.10, 1.12, 1.13",
                        StartDateTime = DateTime.Now,
                        StopDateTime = DateTime.Now.AddDays(1),
                        Group = diaryDbContext.Groups.FirstOrDefault(p => p.Id == 1),
                        Lesson = diaryDbContext.Lessons.FirstOrDefault(l => l.Id == 1),

                    }
                );

                diaryDbContext.SaveChanges();
            }

            var homework = diaryDbContext.Homeworks.First(h => h.Id == 1);
            var student = diaryDbContext.Students.First(s => s.Id == 3);

            if (!diaryDbContext.ReadyHomeworks.Any())
            {
                diaryDbContext.ReadyHomeworks.AddRange(
                   new ReadyHomework()
                    {
                        ShortDescription = homework.ShortDescription,
                        LongDescription = homework.LongDescription,
                        StartDateTime = homework.StartDateTime,
                        StopDateTime = homework.StopDateTime,
                        DeliveryDateTime = DateTime.Now,
                        Assessment = new Assessment() { Mark = 8 },
                        Group = homework.Group,
                        Lesson = homework.Lesson,
                        Homework = homework,
                        Student = student,
                    }
                );

                diaryDbContext.SaveChanges();
            }

            if (!diaryDbContext.Assessments.Any())
            {
                diaryDbContext.Assessments.AddRange(
                    new Assessment() { Mark = 1 },
                    new Assessment() { Mark = 2 },
                    new Assessment() { Mark = 3 },
                    new Assessment() { Mark = 4 },
                    new Assessment() { Mark = 5 },
                    new Assessment() { Mark = 6 },
                    new Assessment() { Mark = 7 },
                    new Assessment() { Mark = 8 },
                    new Assessment() { Mark = 9 },
                    new Assessment() { Mark = 10 },
                    new Assessment() { Mark = 11 },
                    new Assessment() { Mark = 12 }
                );

                diaryDbContext.SaveChanges();
            }
        }
    }
}
