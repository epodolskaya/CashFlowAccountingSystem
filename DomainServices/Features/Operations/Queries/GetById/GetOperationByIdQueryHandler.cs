using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Queries.GetById;

public class GetOperationByIdQueryHandler : IRequestHandler<GetOperationByIdQuery, Operation>
{
    private readonly IReadOnlyRepository<Operation> _operationRepository;

    public GetOperationByIdQueryHandler(IReadOnlyRepository<Operation> operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task<Operation> Handle(GetOperationByIdQuery request, CancellationToken cancellationToken)
    {
        Operation? operation = await _operationRepository.GetFirstOrDefaultAsync
                                   (x => x.Id == request.Id,
                                    x => x.Include(c => c.Category)
                                          .Include(c => c.Type),
                                    cancellationToken);

        if (operation is null)
        {
            throw new EntityNotFoundException($"{nameof(Operation)} with id:{request.Id} doesn't exist.");
        }

        return operation;
    }
}