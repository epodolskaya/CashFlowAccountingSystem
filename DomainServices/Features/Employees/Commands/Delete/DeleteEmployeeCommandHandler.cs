using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly AccountingSystemContext _repository;

    public DeleteEmployeeCommandHandler(AccountingSystemContext employeeRepository)
    {
        _repository = employeeRepository;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToDelete = await _repository.Employees.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (employeeToDelete is not null)
        {
            _repository.Employees.Remove(employeeToDelete);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}