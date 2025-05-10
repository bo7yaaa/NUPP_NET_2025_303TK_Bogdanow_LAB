using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagement.Common.Models;

public class Course
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Credits { get; set; }

    // конструктор
    public Course()
    {
        Id = Guid.NewGuid();
    }
}