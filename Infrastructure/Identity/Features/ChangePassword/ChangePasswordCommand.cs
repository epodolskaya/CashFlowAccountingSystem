using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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