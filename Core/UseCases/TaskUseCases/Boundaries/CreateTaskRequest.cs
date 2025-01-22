using MediatR;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed class CreateTaskRequest(string Tittle, string? Description, DateTime DueDate) : IRequest<CreateTaskResponse>;
