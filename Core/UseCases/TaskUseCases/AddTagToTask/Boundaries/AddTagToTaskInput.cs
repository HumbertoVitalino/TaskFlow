using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;

public sealed record AddTagToTaskInput(int TaskId, int TagId) : IRequest<TaskTagOutput>;
