using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;

public class GetOperationCategoriesByDepartmentIdQuery : IRequest<ICollection<OperationCategory>>
{
    public long DepartmentId { get; init; }

    public GetOperationCategoriesByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }
}