using ApplicationCore.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Operations.GetByDepartmentId;
public class GetOperationsByDepartmentIdQuery : IRequest<ICollection<Operation>>
{
    public GetOperationsByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }

    public long DepartmentId { get; init; }
}
