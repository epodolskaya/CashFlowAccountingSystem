using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetById;

public class GetPositionByIdQuery : IRequest<Position>
{
    public long Id { get; set; }

    public GetPositionByIdQuery(long id)
    {
        Id = id;
    }
}