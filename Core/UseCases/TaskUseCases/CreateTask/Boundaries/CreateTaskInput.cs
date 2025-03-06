using Core.Entities;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed record class CreateTaskInput(string Title, string? Description, int UserId, DateTime DueDate, int? TagId)
    : IRequest<TaskOutput>;
