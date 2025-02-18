using Core.UseCases.SubTasksUseCases.Output;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;

public sealed record DeleteSubTaskInput(int Id) : IRequest<SubTaskOutput>;
