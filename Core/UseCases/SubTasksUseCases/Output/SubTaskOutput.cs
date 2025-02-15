using Core.Enums;

namespace Core.UseCases.SubTasksUseCases.Output;

public sealed class SubTaskOutput
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public StatusEnum Status { get; set; }
    public int TaskId { get; set; }
}
