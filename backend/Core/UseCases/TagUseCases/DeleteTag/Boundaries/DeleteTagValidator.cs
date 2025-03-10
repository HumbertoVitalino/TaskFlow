using FluentValidation;

namespace Core.UseCases.TagUseCases.DeleteTag.Boundaries;

public class DeleteTagValidator : AbstractValidator<DeleteTagInput>
{
    public DeleteTagValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
