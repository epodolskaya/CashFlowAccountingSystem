using FluentValidation;

namespace Infrastructure.Identity.Features.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.UserName)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}