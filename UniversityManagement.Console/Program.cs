using System;
using UniversityManagement.Common.Models;
using UniversityManagement.Common.Services;

namespace UniversityManagement.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentService = new UniversityService<Student>();
            var teacherService = new UniversityService<Teacher>();

            var student = new Student
            {
                FullName = "Bogdanow Ilya",
                Age = 20,
                Group = "303-TK",
                StudentId = "TA415215t12321"
            };

            var teacher = new Teacher
            {
                FullName = "Tytynnik Petro",
                Age = 25,
                Subject = "Технології на платформі NET ",
                ExperienceYears = 3
            };

            studentService.Create(student);
            teacherService.Create(teacher);

            Console.WriteLine("=== Students ===");
            foreach (var s in studentService.ReadAll())
            {
                Console.WriteLine(s.FullName + ", Group: " + s.Group);
            }

            Console.WriteLine("=== Teachers ===");
            foreach (var t in teacherService.ReadAll())
            {
                Console.WriteLine(t.FullName + ", Subject: " + t.Subject);
            }

            studentService.Save("students.json");
            teacherService.Save("teachers.json");

            Console.WriteLine("Saved to JSON files.");
        }
    }
}
