using FluentValidation;

namespace Core.UseCases.UserUseCases.LoginUser.Boundaries;

public class LoginUserValidator : AbstractValidator<LoginUserInput>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
