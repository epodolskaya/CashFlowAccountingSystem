using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Employees.Queries.GetByDepartmentId;
public class GetEmployeesByDepartmentIdQueryHandler : IRequestHandler<GetEmployeesByDepartmentIdQuery, ICollection<Employee>>
{
    private readonly AccountingSystemContext _repository;

    public GetEmployeesByDepartmentIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Employee>> Handle(GetEmployeesByDepartmentIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Employees
                                .Where(x => x.DepartmentId == request.DepartmentId)
                                .Include(x => x.Department)
                                .Include(x => x.Position)
                                .ToListAsync(cancellationToken);
    }
}
