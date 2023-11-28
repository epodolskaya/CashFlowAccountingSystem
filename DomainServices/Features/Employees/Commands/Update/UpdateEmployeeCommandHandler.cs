using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
{
    private readonly AccountingSystemContext _repository;

    public UpdateEmployeeCommandHandler(AccountingSystemContext employeeRepository)
    {
        _repository = employeeRepository;
    }

    public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToEdit = await _repository.Employees.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (employeeToEdit is null)
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.Id} doesn't exist");
        }

        if (!await IsPositionExistsAsync(request.PositionId))
        {
            throw new EntityNotFoundException($"{nameof(Position)} with id:{request.PositionId} doesn't exist.");
        }

        if (!await IsDepartmentExistsAsync(request.DepartmentId))
        {
            throw new EntityNotFoundException($"{nameof(Department)} with id:{request.DepartmentId} doesn't exist.");
        }

        employeeToEdit.Name = request.Name;
        employeeToEdit.Surname = request.Surname;
        employeeToEdit.DateOfBirth = request.DateOfBirth;
        employeeToEdit.PhoneNumber = request.PhoneNumber;
        employeeToEdit.Salary = request.Salary;
        employeeToEdit.PositionId = request.PositionId;
        employeeToEdit.DepartmentId = request.DepartmentId;

        await _repository.SaveChangesAsync(cancellationToken);

        return employeeToEdit;
    }

    private Task<bool> IsPositionExistsAsync(long positionId)
    {
        return _repository.Positions.AnyAsync(x => x.Id == positionId);
    }

    private Task<bool> IsDepartmentExistsAsync(long departmentId)
    {
        return _repository.Departments.AnyAsync(x => x.Id == departmentId);
    }
}