namespace Core.Entities;

public sealed class Task : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}
