using MediatR;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed record CreateTaskRequest(string Title, string? Description, int? Status, DateTime DueDate) : IRequest<CreateTaskResponse>;
