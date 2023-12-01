using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DomainServices.Features.Operations.Commands.Create;

public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, Operation>
{
    private readonly AccountingSystemContext _repository;

    public CreateOperationCommandHandler(AccountingSystemContext repository)
    {
        _repository = repository;
    }

    private Task<bool> IsCategoryExistsAsync(long categoryId)
    {
        return _repository.OperationCategories.AnyAsync(x => x.Id == categoryId);
    }

    private Task<bool> IsTypeExistsAsync(long typeId)
    {
        return _repository.OperationTypes.AnyAsync(x => x.Id == typeId);
    }

    private Task<bool> IsDepartmentExistsAsync(long departmentId)
    {
        return _repository.Departments.AnyAsync(x => x.Id == departmentId);
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

        if (!await IsDepartmentExistsAsync(request.DepartmentId))
        {
            throw new EntityNotFoundException($"{nameof(Department)} with id:{request.DepartmentId} doesn't exist.");
        }

        Operation operation = new Operation
        {
            TypeId = request.TypeId,
            CategoryId = request.CategoryId,
            Comment = request.Comment,
            Sum = request.Sum,
            Date = request.Date,
            DepartmentId = request.DepartmentId
        };

        EntityEntry<Operation> insertedValue = await _repository.Operations.AddAsync(operation, cancellationToken);

        await _repository.SaveChangesAsync(cancellationToken);

        return insertedValue.Entity;
    }
}