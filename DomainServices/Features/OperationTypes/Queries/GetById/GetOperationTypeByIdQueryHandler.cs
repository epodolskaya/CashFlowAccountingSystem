using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.OperationTypes.Queries.GetById;

public class GetOperationTypeByIdQueryHandler : IRequestHandler<GetOperationTypeByIdQuery, OperationType>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationTypeByIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<OperationType> Handle(GetOperationTypeByIdQuery request, CancellationToken cancellationToken)
    {
        OperationType? operationType = await _repository.OperationTypes.Include(x => x.OperationCategories)
                                                        .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (operationType is null)
        {
            throw new EntityNotFoundException($"{nameof(OperationType)} with id:{request.Id} doesn't exist");
        }

        return operationType;
    }
}