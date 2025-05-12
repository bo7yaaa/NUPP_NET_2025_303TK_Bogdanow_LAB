using System;

namespace UniversityManagement.Common.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
        }
    }
}
