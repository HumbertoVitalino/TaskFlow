using FluentValidation;

namespace Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;

public class AddTagToTaskValidator : AbstractValidator<AddTagToTaskInput>
{
    public AddTagToTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.IdTag).NotEmpty();
    }
}
