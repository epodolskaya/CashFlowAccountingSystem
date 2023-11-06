using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Commands.Delete;

internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IRepository<Employee> _employeeRepository;

    public DeleteEmployeeCommandHandler(IRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToDelete = await _employeeRepository.GetFirstOrDefaultAsync
                                         (predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (employeeToDelete is not null)
        {
            _employeeRepository.Delete(employeeToDelete);
            await _employeeRepository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}