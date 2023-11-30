using ApplicationCore.Entity;
using Infrastructure.Data;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly AccountingSystemContext _repository;

    private readonly IdentityContext _identityContext;

    public DeleteEmployeeCommandHandler(AccountingSystemContext employeeRepository, IdentityContext identityContext)
    {
        _repository = employeeRepository;
        _identityContext = identityContext;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employeeToDelete = await _repository.Employees.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (employeeToDelete is not null)
        {
            ICollection<EmployeeAccount> employeeAccounts = await _identityContext.Users.Where
                                                                                      (x => x.EmployeeId == employeeToDelete.Id)
                                                                                  .ToListAsync(cancellationToken);
            if (employeeAccounts.Count != 0)
            {
                foreach (EmployeeAccount employeeAccount in employeeAccounts)
                {
                    _identityContext.Users.Remove(employeeAccount);
                }

                await _identityContext.SaveChangesAsync(cancellationToken);
            }

            _repository.Employees.Remove(employeeToDelete);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}