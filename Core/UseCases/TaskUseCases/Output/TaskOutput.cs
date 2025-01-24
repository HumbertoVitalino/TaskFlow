using Core.Enums;

namespace Core.UseCases.TaskUseCases.Output;

public sealed class TaskOutput
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
}
