using FluentValidation;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
    }
}
