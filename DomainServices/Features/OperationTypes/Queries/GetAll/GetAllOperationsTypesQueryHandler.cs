using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationTypes.Queries.GetAll;

public class GetAllOperationsTypesQueryHandler : IRequestHandler<GetAllOperationsTypesQuery, ICollection<OperationType>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllOperationsTypesQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<OperationType>> Handle(GetAllOperationsTypesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.OperationTypes.AsNoTracking().ToListAsync(cancellationToken);
    }
}