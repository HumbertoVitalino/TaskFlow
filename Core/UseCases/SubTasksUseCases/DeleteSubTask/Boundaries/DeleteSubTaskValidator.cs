using FluentValidation;

namespace Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;

public class DeleteSubTaskValidator : AbstractValidator<DeleteSubTaskInput>
{
	public DeleteSubTaskValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
