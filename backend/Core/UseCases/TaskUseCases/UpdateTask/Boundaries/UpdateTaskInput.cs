using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.UpdateTask.Boundaries;

public sealed record UpdateTaskInput(int Id, string? Title, string? Description, DateTime? DueDate, int UserId) : IRequest<TaskOutput>;
