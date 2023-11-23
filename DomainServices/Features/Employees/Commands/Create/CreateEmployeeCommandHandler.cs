using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DomainServices.Features.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly AccountingSystemContext _repository;

    public CreateEmployeeCommandHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (!await IsPositionExistsAsync(request.PositionId))
        {
            throw new EntityNotFoundException($"{nameof(Position)} with id:{request.PositionId} doesn't exist.");
        }

        Employee employee = new Employee
        {
            Name = request.Name,
            Surname = request.Surname,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Salary = request.Salary,
            PositionId = request.PositionId
        };

        EntityEntry<Employee> insertedValue = await _repository.Employees.AddAsync(employee, cancellationToken);

        await _repository.SaveChangesAsync(cancellationToken);

        return insertedValue.Entity;
    }

    private Task<bool> IsPositionExistsAsync(long positionId)
    {
        return _repository.Positions.AnyAsync(x => x.Id == positionId);
    }
}