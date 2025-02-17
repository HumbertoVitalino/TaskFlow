using FluentValidation;

namespace Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;

public class UpdateSubTaskValidator : AbstractValidator<UpdateSubTaskInput>
{
	public UpdateSubTaskValidator()
	{
		RuleFor(x => x.IdSubTask).NotEmpty();
		RuleFor(x => x.Status).NotEmpty();
	}
}
