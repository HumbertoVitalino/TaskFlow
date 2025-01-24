﻿using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed record CreateTaskInput(string Title, string? Description, DateTime DueDate) : IRequest<TaskOutput>;
