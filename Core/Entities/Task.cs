namespace Core.Entities;

public sealed class Task : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public void Complete() => IsCompleted = true;
}
