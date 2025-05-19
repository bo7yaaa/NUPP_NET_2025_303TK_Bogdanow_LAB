namespace University.Infrastructure.Models;

public class StudentModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int GroupId { get; set; }
    public GroupModel Group { get; set; }
}
