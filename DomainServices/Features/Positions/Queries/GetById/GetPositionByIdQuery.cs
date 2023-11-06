using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetById;

internal class GetPositionByIdQuery : IRequest<Position>
{
    public long Id { get; set; }
}