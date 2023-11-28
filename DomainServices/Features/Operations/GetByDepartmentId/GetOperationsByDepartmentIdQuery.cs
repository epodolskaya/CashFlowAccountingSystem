using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.GetByDepartmentId;

public class GetOperationsByDepartmentIdQuery : IRequest<ICollection<Operation>>
{
    public GetOperationsByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }

    public long DepartmentId { get; init; }
}