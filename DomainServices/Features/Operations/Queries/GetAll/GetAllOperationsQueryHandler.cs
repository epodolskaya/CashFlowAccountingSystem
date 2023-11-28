using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Queries.GetAll;

public class GetAllOperationsQueryHandler : IRequestHandler<GetAllOperationsQuery, ICollection<Operation>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllOperationsQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Operation>> Handle(GetAllOperationsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.Operations.Include
                                    (x => x.Category)
                                .Include(x => x.Type)
                                .Include(x => x.Department)
                                .ToListAsync(cancellationToken);
    }
}