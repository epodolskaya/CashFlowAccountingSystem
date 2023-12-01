using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.GetByDepartmentId;

public class GetOperationsByDepartmentIdQuery : IRequest<ICollection<Operation>>
{
    public long DepartmentId { get; init; }

    public GetOperationsByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }
}