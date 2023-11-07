using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Create;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Employee>
{
    private readonly IRepository<Employee> _employeeRepository;

    private readonly IRepository<Position> _positionsRepository;

    public CreateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IRepository<Position> positionsRepository)
    {
        _employeeRepository = employeeRepository;
        _positionsRepository = positionsRepository;
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

        Employee insertedValue = await _employeeRepository.InsertAsync(employee, cancellationToken);

        await _employeeRepository.SaveChangesAsync(cancellationToken);

        return insertedValue;
    }

    private Task<bool> IsPositionExistsAsync(long positionId)
    {
        return _positionsRepository.ExistsAsync(x => x.Id == positionId);
    }
}