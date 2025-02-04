using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;

public sealed record AddTagToTaskInput(int Id, int IdTag) : IRequest<TaskOutput>;
