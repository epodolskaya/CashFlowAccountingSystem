using Infrastructure.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.SignOut;

public class SignOutCommandHandler : IRequestHandler<SignOutCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    public SignOutCommandHandler(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _authorizationService.SingOutAsync();

        return Unit.Value;
    }
}