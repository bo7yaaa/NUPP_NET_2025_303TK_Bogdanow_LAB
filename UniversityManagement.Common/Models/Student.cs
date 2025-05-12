using System;

namespace UniversityManagement.Common.Models
{
    public class Student : Person
    {
        public string StudentId { get; set; }
        public string Group { get; set; }

        public static int TotalStudents;
        private static Random _rnd = new Random();

        static Student()
        {
            TotalStudents = 0;
        }

        public Student()
        {
            TotalStudents++;
        }

        public static Student CreateNew()
        {
            return new Student
            {
                FullName = "Student " + Guid.NewGuid().ToString().Substring(0, 5),
                Age = _rnd.Next(18, 30),
                Group = "TK-" + _rnd.Next(1, 5),
                StudentId = "TA-" + _rnd.Next(1000, 9999)
            };
        }
    }
}
