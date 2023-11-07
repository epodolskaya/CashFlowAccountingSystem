using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetAll;

public class GetAllOperationsQueryHandler : IRequestHandler<GetAllOperationsQuery, ICollection<Operation>>
{
    private readonly IReadOnlyRepository<Operation> _operationRepository;

    public GetAllOperationsQueryHandler(IReadOnlyRepository<Operation> operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public Task<ICollection<Operation>> Handle(GetAllOperationsQuery request, CancellationToken cancellationToken)
    {
        return _operationRepository.GetAllAsync(cancellationToken);
    }
}