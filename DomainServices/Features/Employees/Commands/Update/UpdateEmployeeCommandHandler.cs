using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
{
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Position> _positionsRepository;

    public UpdateEmployeeCommandHandler(IRepository<Employee> employeeRepository, IRepository<Position> positionsRepository)
    {
        _employeeRepository = employeeRepository;
        _positionsRepository = positionsRepository;
    }

    public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToEdit = await _employeeRepository.GetFirstOrDefaultAsync
                                       (predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);
        
        if (employeeToEdit is null)
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.Id} doesn't exist");
        }

        if (!await IsPositionExistsAsync(request.PositionId))
        {
            throw new EntityNotFoundException($"{nameof(Position)} with id:{request.PositionId} doesn't exist.");
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

    private Task<bool> IsPositionExistsAsync(long positionId)
    {
        return _positionsRepository.ExistsAsync(x => x.Id == positionId);
    }
}