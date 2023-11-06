using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

internal class GetOperationCategoryByIdQueryHandler : IRequestHandler<GetOperationCategoryByIdQuery, OperationCategory>
{
    private readonly IReadOnlyRepository<OperationCategory> _operationCategoryRepository;

    public GetOperationCategoryByIdQueryHandler(IReadOnlyRepository<OperationCategory> operationCategoryRepository)
    {
        _operationCategoryRepository = operationCategoryRepository;
    }

    public async Task<OperationCategory> Handle(GetOperationCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        OperationCategory? category = await _operationCategoryRepository.GetFirstOrDefaultAsync
                                          (x => x.Id == request.Id, x => x.Include(c => c.Operations), cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"{nameof(OperationCategory)} with id:{request.Id} doesn't exist");
        }

        return category;
    }
}