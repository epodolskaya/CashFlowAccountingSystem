using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetAll;

public class GetAllOperationCategoriesQueryHandler
    : IRequestHandler<GetAllOperationCategoriesQuery, ICollection<OperationCategory>>
{
    private readonly IReadOnlyRepository<OperationCategory> _operationCategoriesRepository;

    public GetAllOperationCategoriesQueryHandler(IReadOnlyRepository<OperationCategory> operationCategoriesRepository)
    {
        _operationCategoriesRepository = operationCategoriesRepository;
    }

    public async Task<ICollection<OperationCategory>> Handle(GetAllOperationCategoriesQuery request,
                                                             CancellationToken cancellationToken)
    {
        return await _operationCategoriesRepository.GetAllAsync(cancellationToken);
    }
}