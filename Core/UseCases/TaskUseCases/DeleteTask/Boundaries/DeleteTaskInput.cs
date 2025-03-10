using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.DeleteTask.Boundaries;

public sealed record DeleteTaskInput(int Id, int UserId) : IRequest<TaskOutput>;
