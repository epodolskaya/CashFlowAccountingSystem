using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Operations.Commands.Delete;

public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, Unit>
{
    private readonly IRepository<Operation> _repository;

    public DeleteOperationCommandHandler(IRepository<Operation> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
    {
        Operation? operationToDelete = await _repository.GetFirstOrDefaultAsync
                                           (predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (operationToDelete is not null)
        {
            _repository.Delete(operationToDelete);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}