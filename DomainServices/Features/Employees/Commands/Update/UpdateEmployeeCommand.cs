using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Update;

internal class UpdateEmployeeCommand : IRequest<Employee>
{
    public long Id { get; set; }

    public string Name { get; init; }

    public string Surname { get; init; }

    public DateTime DateOfBirth { get; init; }

    public string PhoneNumber { get; init; }

    public decimal Salary { get; init; }

    public long PositionId { get; init; }
}