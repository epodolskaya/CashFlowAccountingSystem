using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Departments.Queries.GetAll;
internal class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ICollection<Department>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllDepartmentsQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Departments.ToListAsync(cancellationToken);
    }
}
