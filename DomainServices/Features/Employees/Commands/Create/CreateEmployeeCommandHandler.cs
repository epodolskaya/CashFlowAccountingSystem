using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IRepository<Employee> _employeeRepository;

    public CreateEmployeeCommandHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee employee = new Employee
        {
            Name = request.Name,
            Surname = request.Surname,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Salary = request.Salary,
            PositionId = request.PositionId
        };

        Employee insertedValue = await _employeeRepository.InsertAsync(employee, cancellationToken);

        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return insertedValue;
    }
}