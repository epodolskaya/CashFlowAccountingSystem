using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Queries.GetById;

public class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, Operation>
{
    private readonly AccountingSystemContext _repository;

    public GetOperationByIdQueryHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<Operation> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken)
    {
        Operation? operation = await _repository.Operations.Include
                                                    (x => x.Category)
                                                .Include(x => x.Type)
                                                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (operation is null)
        {
            throw new EntityNotFoundException($"{nameof(Operation)} with id:{request.Id} doesn't exist.");
        }

        return operation;
    }
}