using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly AccountingSystemContext _repository;

    public GetEmployeeByIdQueryHandler(AccountingSystemContext employeeRepository)
    {
        _repository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Employee? employee = await _repository.Employees.Include
                                                  (x => x.Position).Include(x=>x.Department)
                                              .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (employee is null)
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.Id} doesn't exist.");
        }

        return employee;
    }
}