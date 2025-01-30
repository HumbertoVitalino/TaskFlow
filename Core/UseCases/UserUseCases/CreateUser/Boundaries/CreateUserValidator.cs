using FluentValidation;

namespace Core.UseCases.UserUseCases.CreateUser.Boundaries;

public class CreateUserValidator : AbstractValidator<CreateUserInput>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Confirmation).Equal(x => x.Password);
    }
}
