namespace Core.Entities;

public sealed class Tag : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<TaskTag> TaskTags { get; set; }

}
