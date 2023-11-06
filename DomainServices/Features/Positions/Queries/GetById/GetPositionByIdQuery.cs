using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetById;

public class GetPositionByIdQuery : IRequest<Position>
{
    public GetPositionByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}