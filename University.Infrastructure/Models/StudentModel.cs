namespace University.Infrastructure.Models;

public class StudentModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid GroupId { get; set; }
    public GroupModel Group { get; set; }
}
