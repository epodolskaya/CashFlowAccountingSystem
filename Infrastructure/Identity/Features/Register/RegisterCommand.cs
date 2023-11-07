using MediatR;

namespace Infrastructure.Identity.Features.Register;

public class RegisterCommand : IRequest<Unit>
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string ConfirmPassword { get; init; }

    public long EmployeeId { get; set; }
}