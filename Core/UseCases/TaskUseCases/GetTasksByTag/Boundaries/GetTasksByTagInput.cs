using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;

public sealed record GetTasksByTagInput(int idTag) : IRequest<List<TaskOutput>>;