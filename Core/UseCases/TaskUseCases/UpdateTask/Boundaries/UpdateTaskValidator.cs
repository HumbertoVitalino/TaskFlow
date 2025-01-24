using FluentValidation;

namespace Core.UseCases.TaskUseCases.UpdateTask.Boundaries;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskInput>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
    }
}
