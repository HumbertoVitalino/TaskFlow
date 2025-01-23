namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed class CreateTaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Status { get; set; } = 1;
}
