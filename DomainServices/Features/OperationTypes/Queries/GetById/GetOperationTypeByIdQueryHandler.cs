using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationTypes.Queries.GetById;

internal class GetOperationTypeByIdQueryHandler : IRequestHandler<GetOperationTypeByIdQuery, OperationType>
{
    private readonly IReadOnlyRepository<OperationType> _operationTypesRepository;

    public GetOperationTypeByIdQueryHandler(IReadOnlyRepository<OperationType> operationTypesRepository)
    {
        _operationTypesRepository = operationTypesRepository;
    }

    public async Task<OperationType> Handle(GetOperationTypeByIdQuery request, CancellationToken cancellationToken)
    {
        OperationType? operationType = await _operationTypesRepository.GetFirstOrDefaultAsync
                                           (x => x.Id == request.Id,
                                            x => x.Include(c => c.Operations),
                                            cancellationToken);

        if (operationType is null)
        {
            throw new EntityNotFoundException($"{nameof(OperationType)} with id:{request.Id} doesn't exist");
        }

        return operationType;
    }
}