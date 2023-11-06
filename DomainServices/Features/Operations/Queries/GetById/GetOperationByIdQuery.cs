using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetById;

public class GetOperationByIdQuery : IRequest<Operation>
{
    public GetOperationByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}