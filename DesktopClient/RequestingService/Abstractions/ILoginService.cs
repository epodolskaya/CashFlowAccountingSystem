using DesktopClient.Commands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.RequestingService.Abstractions;
internal interface ILoginService
{
    Task SignInAsync(SignInCommand command);
    Task RegisterAsync(RegisterCommand command);
    Task SignOutAsync();
}