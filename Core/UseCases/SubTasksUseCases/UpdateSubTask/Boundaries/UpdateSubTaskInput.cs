using Core.Enums;
using Core.UseCases.SubTasksUseCases.Output;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;

public sealed record UpdateSubTaskInput(int IdSubTask, string? Title, DateTime? DueDate, StatusEnum? Status, int UserId) : IRequest<SubTaskOutput>;
