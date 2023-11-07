using DesktopClient.Commands.Abstractions;

namespace DesktopClient.Commands.Employee;

public class UpdateEmployeeCommand : UpdateCommand<Entity.Employee>
{
    public string Name { get; init; }

    public string Surname { get; init; }

    public DateTime DateOfBirth { get; init; }

    public string PhoneNumber { get; init; }

    public decimal Salary { get; init; }

    public long PositionId { get; init; }
}