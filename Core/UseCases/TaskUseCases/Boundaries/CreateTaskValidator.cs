using FluentValidation;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator()
    {
    }
}
