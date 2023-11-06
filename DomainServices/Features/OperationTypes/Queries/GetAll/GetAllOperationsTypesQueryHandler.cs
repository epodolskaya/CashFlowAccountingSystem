using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.OperationTypes.Queries.GetAll;

public class GetAllOperationsTypesQueryHandler : IRequestHandler<GetAllOperationsTypesQuery, ICollection<OperationType>>
{
    private readonly IReadOnlyRepository<OperationType> _operationRepository;

    public GetAllOperationsTypesQueryHandler(IReadOnlyRepository<OperationType> operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<ICollection<OperationType>> Handle(GetAllOperationsTypesQuery request, CancellationToken cancellationToken)
    {
        return await _operationRepository.GetAllAsync(cancellationToken);
    }
}