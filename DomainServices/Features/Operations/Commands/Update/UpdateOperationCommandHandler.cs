using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Operations.Commands.Update;

public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, Operation>
{
    private readonly IRepository<OperationCategory> _operationCategory;

    private readonly IRepository<OperationType> _operationType;
    private readonly IRepository<Operation> _repository;

    public UpdateOperationCommandHandler(IRepository<Operation> repository,
                                         IRepository<OperationCategory> operationCategory,
                                         IRepository<OperationType> operationType)
    {
        _repository = repository;
        _operationCategory = operationCategory;
        _operationType = operationType;
    }

    public async Task<Operation> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
    {
        Operation? operationToUpdate = await _repository.GetFirstOrDefaultAsync
                                           (predicate: x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (operationToUpdate is null)
        {
            throw new EntityNotFoundException($"{nameof(Operation)} with id:{request.Id} doesn't exist.");
        }

        if (!IsCategoryExists(request.CategoryId))
        {
            throw new EntityNotFoundException($"{nameof(OperationCategory)} with id:{request.CategoryId} doesn't exist.");
        }

        if (!IsTypeExists(request.TypeId))
        {
            throw new EntityNotFoundException($"{nameof(OperationType)} with id:{request.TypeId} doesn't exist.");
        }

        operationToUpdate.TypeId = request.TypeId;
        operationToUpdate.CategoryId = request.CategoryId;
        operationToUpdate.Comment = request.Comment;
        operationToUpdate.Sum = request.Sum;
        operationToUpdate.Date = request.Date;

        await _repository.SaveChangesAsync(cancellationToken);

        return operationToUpdate;
    }

    private bool IsCategoryExists(long categoryId)
    {
        return _operationCategory.Exists(x => x.Id == categoryId);
    }

    private bool IsTypeExists(long typeId)
    {
        return _operationType.Exists(x => x.Id == typeId);
    }
}