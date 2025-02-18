using Core.UseCases.SubTasksUseCases.Output;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;

public sealed record CreateSubTaskInput(string Title, DateTime DueDate, int TaskId) : IRequest<SubTaskOutput>;