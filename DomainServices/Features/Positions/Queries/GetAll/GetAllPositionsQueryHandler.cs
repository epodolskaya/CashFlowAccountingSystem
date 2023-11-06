using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetAll;

internal class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, ICollection<Position>>
{
    private readonly IReadOnlyRepository<Position> _positionsRepository;

    public GetAllPositionsQueryHandler(IReadOnlyRepository<Position> positionsRepository)
    {
        _positionsRepository = positionsRepository;
    }

    public async Task<ICollection<Position>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _positionsRepository.GetAllAsync(cancellationToken);
        ;
    }
}