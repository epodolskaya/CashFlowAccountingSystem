using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;

public class GetOperationCategoriesByDepartmentIdQueryHandler
    : IRequestHandler<GetOperationCategoriesByDepartmentIdQuery, ICollection<OperationCategory>>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationCategoriesByDepartmentIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<OperationCategory>> Handle(GetOperationCategoriesByDepartmentIdQuery request,
                                                             CancellationToken cancellationToken)
    {
        return await _repository.OperationCategories.Where(x => x.Departments.Select(c => c.Id).Contains(request.DepartmentId))
                                .Include(x => x.Departments)
                                .Include(x => x.Operations)
                                .ToListAsync(cancellationToken);
    }
}