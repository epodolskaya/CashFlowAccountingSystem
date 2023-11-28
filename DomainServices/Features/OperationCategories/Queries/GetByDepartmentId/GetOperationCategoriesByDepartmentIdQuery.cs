using ApplicationCore.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;
public class GetOperationCategoriesByDepartmentIdQuery : IRequest<ICollection<OperationCategory>>
{
    public GetOperationCategoriesByDepartmentIdQuery(long departmentId) {
        DepartmentId = departmentId;
    }

    public long DepartmentId { get; init; }
}
