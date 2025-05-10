using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagement.Common.Models;

public class Student : Person
{
    public string StudentId { get; set; }
    public string Group { get; set; }

    // статичне поле
    public static int TotalStudents;

    // статичний конструктор
    static Student()
    {
        TotalStudents = 0;
    }

    public Student()
    {
        TotalStudents++;
    }

    // метод
    public void Study()
    {
        Console.WriteLine($"{FullName} is studying.");
    }
}