using FluentValidation;

namespace Core.UseCases.TaskUseCases.Boundaries;

public sealed class CreateTaskValidator : AbstractValidator<CreateTaskInput>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
    }
}
