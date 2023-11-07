using FluentValidation;

namespace Infrastructure.Identity.Features.DeleteAccount;

internal class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
    }
}