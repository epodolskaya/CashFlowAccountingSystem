using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationCategories.Queries.GetAll;

public class GetAllOperationCategoriesQueryHandler
    : IRequestHandler<GetAllOperationCategoriesQuery, ICollection<OperationCategory>>
{
    private readonly AccountingSystemContext _repository;

    public GetAllOperationCategoriesQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<OperationCategory>> Handle(GetAllOperationCategoriesQuery request,
                                                       CancellationToken cancellationToken)
    {
        return await _repository.OperationCategories.ToListAsync(cancellationToken);
    }
}