using ApplicationCore.Entity;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Commands.Delete;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, Unit>
{
    private readonly AccountingSystemContext _repository;

    public DeleteOperationCommandHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        Operation? operationToDelete = await _repository.Operations.SingleOrDefaultAsync
                                           (x => x.Id == request.Id, cancellationToken);

        if (operationToDelete is not null)
        {
            _repository.Operations.Remove(operationToDelete);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}