using DesktopClient.Commands.Login;

namespace DesktopClient.RequestingService.Abstractions;

internal interface ILoginService
{
    Roles GetRole();

    Task SignInAsync(SignInCommand command);

    Task RegisterAsync(RegisterCommand command);

    Task SignOutAsync();
}