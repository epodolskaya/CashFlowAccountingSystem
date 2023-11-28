using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Operations.GetByDepartmentId;
public class GetOperationsByDepartmentIdQueryHandler : IRequestHandler<GetOperationsByDepartmentIdQuery, ICollection<Operation>>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationsByDepartmentIdQueryHandler(AccountingSystemContext repository) 
    {
        _repository = repository;
    }

    public async Task<ICollection<Operation>> Handle(GetOperationsByDepartmentIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Operations.Where(x => x.DepartmentId == request.DepartmentId)
                          .Include(x => x.Type)
                          .Include(x => x.Category)
                          .Include(x => x.Department)
                          .ToListAsync(cancellationToken);
    }
}
