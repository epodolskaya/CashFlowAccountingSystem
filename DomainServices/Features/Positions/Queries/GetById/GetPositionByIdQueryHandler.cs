using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Positions.Queries.GetById;

public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Position>
{
    private readonly AccountingSystemContext _repository;

    public GetPositionByIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<Position> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
    {
        Position? position = await _repository.Positions
                                              .AsNoTracking()
                                              .Include(x => x.Employees)
                                              .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (position is null)
        {
            throw new EntityNotFoundException($"{nameof(Position)} with id:{request.Id} doesn't exists.");
        }

        return position;
    }
}