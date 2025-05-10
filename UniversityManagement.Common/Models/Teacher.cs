using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagement.Common.Models;

public class Teacher : Person
{
    public string Subject { get; set; }
    public int ExperienceYears { get; set; }

    // метод
    public void Teach()
    {
        Console.WriteLine($"{FullName} is teaching {Subject}.");
    }
}