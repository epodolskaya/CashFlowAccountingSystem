using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Operations.Commands.Update;

public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, Operation>
{
    private readonly AccountingSystemContext _repository;

    public UpdateOperationCommandHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    public async Task<Operation> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
    {
        Operation? operationToUpdate = await _repository.Operations.SingleOrDefaultAsync
                                           (x => x.Id == request.Id, cancellationToken);

        if (operationToUpdate is null)
        {
            throw new EntityNotFoundException($"{nameof(Operation)} with id:{request.Id} doesn't exist.");
        }

        if (!await IsCategoryExistsAsync(request.CategoryId))
        {
            throw new EntityNotFoundException($"{nameof(OperationCategory)} with id:{request.CategoryId} doesn't exist.");
        }

        if (!await IsTypeExistsAsync(request.TypeId))
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

    private Task<bool> IsCategoryExistsAsync(long categoryId)
    {
        return _repository.OperationCategories.AnyAsync(x => x.Id == categoryId);
    }

    private Task<bool> IsTypeExistsAsync(long typeId)
    {
        return _repository.OperationTypes.AnyAsync(x => x.Id == typeId);
    }
}