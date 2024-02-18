using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetByDepartmentId;

public class GetOperationsByDepartmentIdQuery : IRequest<ICollection<Operation>>
{
    public GetOperationsByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }

    public long DepartmentId { get; init; }
}