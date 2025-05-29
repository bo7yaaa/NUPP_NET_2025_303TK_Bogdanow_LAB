using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using University.Infrastructure.Models;

namespace University.Infrastructure;

public class UniversityContext(DbContextOptions<UniversityContext> options) : DbContext(options)
{
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
