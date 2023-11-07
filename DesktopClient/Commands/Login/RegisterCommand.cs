namespace DesktopClient.Commands.Login;

public class RegisterCommand
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string ConfirmPassword { get; init; }

    public long EmployeeId { get; set; }
}