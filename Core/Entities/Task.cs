using Core.Enums;

namespace Core.Entities;

public sealed class Task : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
    public ICollection<SubTask> SubTasks { get; set; }
    public Tag? Tag { get; set; }
    public User User { get; set; }

    public Task()
    {
        Status = StatusEnum.Pending;
    }
}
