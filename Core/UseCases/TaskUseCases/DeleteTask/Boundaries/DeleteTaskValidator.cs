using FluentValidation;

namespace Core.UseCases.TaskUseCases.DeleteTask.Boundaries;

public class DeleteTaskValidator : AbstractValidator<DeleteTaskInput>
{
    public DeleteTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
