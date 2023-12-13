using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

public class GetOperationCategoryByIdQueryHandler : IRequestHandler<GetOperationCategoryByIdQuery, OperationCategory>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationCategoryByIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<OperationCategory> Handle(GetOperationCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        OperationCategory? category = await _repository.OperationCategories
                                                       .AsNoTracking()
                                                       .Include(x => x.Operations)
                                                       .Include(x => x.Departments)
                                                       .Include(x => x.Type)
                                                       .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"{nameof(OperationCategory)} with id:{request.Id} doesn't exist");
        }

        return category;
    }
}