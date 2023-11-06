using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
{
    private readonly IRepository<Employee> _employeeRepository;

    public UpdateEmployeeCommandHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToEdit = await _employeeRepository.GetFirstOrDefaultAsync
                                       (predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employeeToEdit is null)
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.Id} doesn't exist");
        }

        employeeToEdit.Name = request.Name;
        employeeToEdit.Surname = request.Surname;
        employeeToEdit.DateOfBirth = request.DateOfBirth;
        employeeToEdit.PhoneNumber = request.PhoneNumber;
        employeeToEdit.Salary = request.Salary;
        employeeToEdit.PositionId = request.PositionId;

        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return employeeToEdit;
    }
}