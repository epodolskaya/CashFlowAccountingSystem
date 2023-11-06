using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetById;

internal class GetOperationByIdQuery : IRequest<Operation>
{
    public long Id { get; set; }
}