using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Queries.GetByDepartmentId;

public class GetOperationsByDepartmentIdQueryHandler : IRequestHandler<GetOperationsByDepartmentIdQuery, ICollection<Operation>>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationsByDepartmentIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<Operation>> Handle(GetOperationsByDepartmentIdQuery request,
                                                     CancellationToken cancellationToken)
    {
        return await _repository.Operations
                                .AsNoTracking()
                                .Where(x => x.DepartmentId == request.DepartmentId)
                                .Include(x => x.Category)
                                .ThenInclude(x => x.Type)
                                .Include(x => x.Department)
                                .ToListAsync(cancellationToken);
    }
}