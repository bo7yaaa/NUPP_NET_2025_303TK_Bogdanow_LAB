using Microsoft.EntityFrameworkCore;
using University.Infrastructure;
using University.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Додати DbContext і Repo
builder.Services.AddDbContext<UniversityContext>(options =>
    options.UseSqlite("Data Source=university.db"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
