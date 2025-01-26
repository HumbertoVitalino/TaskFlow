using Core.Enums;

namespace Core.Entities;

public sealed class Task : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
    public ICollection<TaskTag> TaskTags { get; set; }

    public Task()
    {
        Status = StatusEnum.Pending;
    }
}
