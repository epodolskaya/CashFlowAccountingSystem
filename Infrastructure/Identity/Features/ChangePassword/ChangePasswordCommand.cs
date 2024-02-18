using MediatR;

namespace Infrastructure.Identity.Features.ChangePassword;

public class ChangePasswordCommand : IRequest<Unit>
{
    public ChangePasswordCommand(string oldPassword, string newPassword)
    {
        NewPassword = newPassword;
        OldPassword = oldPassword;
    }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}