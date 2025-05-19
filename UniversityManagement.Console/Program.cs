using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using University.Infrastructure;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;

var services = new ServiceCollection();

services.AddDbContext<UniversityContext>(options =>
    options.UseSqlite("Data Source=university.db"));

services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

var provider = services.BuildServiceProvider();

var db = provider.GetRequiredService<UniversityContext>();
await db.Database.EnsureCreatedAsync();

var repo = provider.GetRequiredService<IRepository<StudentModel>>();

var group = new GroupModel { Code = "303-TK" };
var student = new StudentModel { Name = "Богданов Ілля", Group = group };

await repo.AddAsync(student);
await repo.SaveAsync();

var all = await repo.GetAllAsync();

foreach (var s in all)
{
    Console.WriteLine($"Student: {s.Name}, Group: {s.Group?.Code}");
}
