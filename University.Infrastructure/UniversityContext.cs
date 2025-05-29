using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Models;

namespace University.Infrastructure
{
    public class UniversityContext : IdentityDbContext<ApplicationUser>
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options) { }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
    }
}
