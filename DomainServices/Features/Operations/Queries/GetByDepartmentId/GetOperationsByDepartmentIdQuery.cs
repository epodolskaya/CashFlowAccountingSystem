using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetByDepartmentId;

public class GetOperationsByDepartmentIdQuery : IRequest<ICollection<Operation>>
{
    public long DepartmentId { get; init; }

    public GetOperationsByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }
}