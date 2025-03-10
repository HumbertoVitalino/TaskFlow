using FluentValidation;

namespace Core.UseCases.TagUseCases.CreateTag.Boundaries;

public sealed class CreateTagValidator : AbstractValidator<CreateTagInput>
{
    public CreateTagValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
