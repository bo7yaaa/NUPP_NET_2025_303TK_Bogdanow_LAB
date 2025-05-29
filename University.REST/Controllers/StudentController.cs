using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;
using University.REST.Models;

namespace University.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Весь контролер вимагає авторизації
public class StudentController : ControllerBase
{
    private readonly IRepository<StudentModel> _repo;

    public StudentController(IRepository<StudentModel> repo)
    {
        _repo = repo;
    }

    // Тільки Admin і Teacher
    [HttpPost]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> Add(StudentDto dto)
    {
        var student = new StudentModel
        {
            Name = dto.Name,
            Group = new GroupModel { Code = dto.GroupCode }
        };

        await _repo.AddAsync(student);
        await _repo.SaveAsync();

        return CreatedAtAction(nameof(GetAll), new { id = student.Id }, student);
    }

    // Дозволено всім
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _repo.GetAllAsync();
        return Ok(students);
    }
}
