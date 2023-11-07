using ApplicationCore.Exceptions;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Features.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Unit>
{
    private readonly UserManager<EmployeeAccount> _userManager;

    public DeleteAccountCommandHandler(UserManager<EmployeeAccount> userManager) {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _userManager.FindByNameAsync(request.UserName);

        if (account is null)
        {
            throw new AuthorizationException($"User with username '{request.UserName}' doesn't exist");
        }

        var result = await _userManager.DeleteAsync(account);

        if (!result.Succeeded)
        {
            throw new AuthorizationException(string.Join(Environment.NewLine, result.Errors));
        }

        return Unit.Value;
    }
}