using FluentValidation;

namespace Core.UseCases.TagUseCases.UpdateTag.Boundaries;

public class UpdateTagValidator : AbstractValidator<UpdateTagInput>
{
    public UpdateTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
