using Core.Enums;

namespace Core.Entities;

public sealed class SubTask : BaseEntity
{
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
    public int TaskId { get; set; }
    public Task Task { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public SubTask() 
    {
        Status = StatusEnum.Pending; 
    }
}
