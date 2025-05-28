using Microsoft.AspNetCore.Mvc;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;
using University.REST.Models;

namespace University.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IRepository<StudentModel> _repo;
    private readonly IRepository<GroupModel> _groupRepo;

    public StudentController(IRepository<StudentModel> repo, IRepository<GroupModel> groupRepo)
    {
        _repo = repo;
        _groupRepo = groupRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Add(StudentDto dto)
    {
        var group = (await _groupRepo.GetAllAsync())
            .FirstOrDefault(g => g.Code == dto.GroupCode)
            ?? new GroupModel { Code = dto.GroupCode };

        var student = new StudentModel
        {
            Name = dto.Name,
            Group = group
        };

        await _repo.AddAsync(student);
        await _repo.SaveAsync();

        return CreatedAtAction(nameof(GetAll), new { id = student.Id }, new
        {
            student.Id,
            student.Name,
            Group = student.Group.Code
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _repo.GetAllAsync();
        var result = students.Select(s => new
        {
            s.Id,
            s.Name,
            Group = s.Group?.Code
        });

        return Ok(result);
    }
}
