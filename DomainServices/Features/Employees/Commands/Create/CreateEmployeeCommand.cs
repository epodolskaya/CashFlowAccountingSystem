using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Create;

internal class CreateEmployeeCommand : IRequest<Employee>
{
    public string Name { get; init; }

    public string Surname { get; init; }

    public DateTime DateOfBirth { get; init; }

    public string PhoneNumber { get; init; }

    public decimal Salary { get; init; }

    public long PositionId { get; init; }
}