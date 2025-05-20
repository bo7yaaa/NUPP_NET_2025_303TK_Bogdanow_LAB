using Microsoft.AspNetCore.Mvc;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;

namespace University.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IRepository<StudentModel> _repository;

    public StudentController(IRepository<StudentModel> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _repository.GetAllAsync();
        return Ok(students);
    }
}
