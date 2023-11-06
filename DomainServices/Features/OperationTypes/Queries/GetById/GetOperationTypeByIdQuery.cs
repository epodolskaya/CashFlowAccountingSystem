using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationTypes.Queries.GetById;

public class GetOperationTypeByIdQuery : IRequest<OperationType>
{
    public GetOperationTypeByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}