using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;

public sealed record GetTasksByTagInput(int idTag, int userId) : IRequest<List<TaskOutput>>;