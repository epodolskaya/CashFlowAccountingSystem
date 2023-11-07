using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Operations.Commands.Create;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, Operation>
{
    private readonly IRepository<OperationCategory> _operationCategory;

    private readonly IRepository<OperationType> _operationType;
    private readonly IRepository<Operation> _repository;

    public CreateOperationCommandHandler(IRepository<Operation> repository,
                                         IRepository<OperationCategory> operationCategory,
                                         IRepository<OperationType> operationType)
    {
        _repository = repository;
        _operationCategory = operationCategory;
        _operationType = operationType;
    }

    public async Task<Operation> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
    {
        if (!await IsCategoryExistsAsync(request.CategoryId))
        {
            throw new EntityNotFoundException($"{nameof(OperationCategory)} with id:{request.CategoryId} doesn't exist.");
        }

        if (!await IsTypeExistsAsync(request.TypeId))
        {
            throw new EntityNotFoundException($"{nameof(OperationType)} with id:{request.TypeId} doesn't exist.");
        }

        Operation operation = new Operation
        {
            TypeId = request.TypeId,
            CategoryId = request.CategoryId,
            Comment = request.Comment,
            Sum = request.Sum,
            Date = request.Date
        };

        Operation inserted = await _repository.InsertAsync(operation, cancellationToken);

        await _repository.SaveChangesAsync(cancellationToken);

        return inserted;
    }

    private Task<bool> IsCategoryExistsAsync(long categoryId)
    {
        return _operationCategory.ExistsAsync(x => x.Id == categoryId);
    }

    private Task<bool> IsTypeExistsAsync(long typeId)
    {
        return _operationType.ExistsAsync(x => x.Id == typeId);
    }
}