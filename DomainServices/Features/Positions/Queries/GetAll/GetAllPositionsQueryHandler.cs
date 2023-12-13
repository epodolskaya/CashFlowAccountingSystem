using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Positions.Queries.GetAll;

public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, ICollection<Position>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllPositionsQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Position>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Positions.AsNoTracking().ToListAsync(cancellationToken);
    }
}