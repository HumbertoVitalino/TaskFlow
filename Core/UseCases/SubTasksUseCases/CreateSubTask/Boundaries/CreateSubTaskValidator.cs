using FluentValidation;

namespace Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;

public sealed class CreateSubTaskValidator : AbstractValidator<CreateSubTaskInput>
{
    public CreateSubTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.DueDate).NotEmpty();
        RuleFor(x => x.TaskId).NotEmpty();
    }
}
