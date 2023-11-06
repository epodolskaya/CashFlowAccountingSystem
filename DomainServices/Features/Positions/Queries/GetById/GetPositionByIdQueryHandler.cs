using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Positions.Queries.GetById;

public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Position>
{
    private readonly IReadOnlyRepository<Position> _positionRepository;

    public GetPositionByIdQueryHandler(IReadOnlyRepository<Position> positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<Position> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        Position? position = await _positionRepository.GetFirstOrDefaultAsync
                                 (x => x.Id == request.Id, x => x.Include(c => c.Employees), cancellationToken);

        if (position is null)
        {
            throw new EntityNotFoundException($"{nameof(Position)} with id:{request.Id} doesn't exists.");
        }

        return position;
    }
}