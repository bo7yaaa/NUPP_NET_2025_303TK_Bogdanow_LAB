namespace University.Infrastructure.Models;

public class GroupModel
{
    public int Id { get; set; }
    public string Code { get; set; }

    public ICollection<StudentModel> Students { get; set; }
}
