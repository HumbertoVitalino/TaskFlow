using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;

public sealed record GetAllTasksInput : IRequest<List<TaskOutput>>;
