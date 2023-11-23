using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Queries.GetAll;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ICollection<Employee>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllEmployeesQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Employees.Include(c => c.Position).ToListAsync(cancellationToken);
    }
}