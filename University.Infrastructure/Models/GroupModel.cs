using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Infrastructure.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;

        public ICollection<StudentModel> Students { get; set; } = new List<StudentModel>();
    }
}
