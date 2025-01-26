namespace Core.Entities;

public sealed class TaskTag
{
    public int TaskId { get; set; }
    public Entities.Task Task { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
