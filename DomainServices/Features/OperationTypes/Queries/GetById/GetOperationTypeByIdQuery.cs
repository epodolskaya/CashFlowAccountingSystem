using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationTypes.Queries.GetById;

internal class GetOperationTypeByIdQuery : IRequest<OperationType>
{
    public long Id { get; set; }
}