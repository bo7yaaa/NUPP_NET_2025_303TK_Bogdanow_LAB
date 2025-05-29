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
var repo = provider.GetRequiredService<IRepository<StudentModel>>();

await repo.AddAsync(new StudentModel
{
    Name = "Богданов Ілля",
    Group = new GroupModel { Code = "303-ТК" }
});

await repo.SaveAsync();

var all = await repo.GetAllAsync();
all.ForEach(s => Console.WriteLine($"{s.Name} - {s.Group?.Code}"));
