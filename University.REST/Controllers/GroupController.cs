using Microsoft.AspNetCore.Mvc;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;

namespace University.REST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IRepository<GroupModel> _repo;

    public GroupController(IRepository<GroupModel> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await _repo.GetAllAsync();
        var result = groups.Select(g => new
        {
            g.Id,
            g.Code,
            StudentCount = g.Students?.Count ?? 0
        });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] string code)
    {
        var group = new GroupModel { Code = code };
        await _repo.AddAsync(group);
        await _repo.SaveAsync();
        return CreatedAtAction(nameof(GetAll), new { id = group.Id }, group);
    }
}
