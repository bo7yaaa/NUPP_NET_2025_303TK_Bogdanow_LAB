using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Models;

namespace University.Infrastructure;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options) { }

    public DbSet<StudentModel> Students => Set<StudentModel>();
    public DbSet<GroupModel> Groups => Set<GroupModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupModel>()
            .HasMany(g => g.Students)
            .WithOne(s => s.Group)
            .HasForeignKey(s => s.GroupId);
    }
}
