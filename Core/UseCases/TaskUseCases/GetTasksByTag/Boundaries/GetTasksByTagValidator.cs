using FluentValidation;

namespace Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;

public class GetTasksByTagValidator : AbstractValidator<GetTasksByTagInput>
{
    public GetTasksByTagValidator()
    {
        RuleFor(x => x.idTag).NotEmpty();
    }
}
