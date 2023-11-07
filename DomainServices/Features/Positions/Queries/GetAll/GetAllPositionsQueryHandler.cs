using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetAll;

public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, ICollection<Position>>
{
    private readonly IReadOnlyRepository<Position> _positionsRepository;

    public GetAllPositionsQueryHandler(IReadOnlyRepository<Position> positionsRepository)
    {
        _positionsRepository = positionsRepository;
    }

    public Task<ICollection<Position>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        return _positionsRepository.GetAllAsync(cancellationToken);
    }
}