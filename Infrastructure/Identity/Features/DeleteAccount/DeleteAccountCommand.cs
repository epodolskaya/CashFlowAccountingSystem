using MediatR;

namespace Infrastructure.Identity.Features.DeleteAccount;

public class DeleteAccountCommand : IRequest<Unit>
{
    public string UserName { get; set; }
}