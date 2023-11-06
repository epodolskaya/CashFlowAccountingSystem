using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
{
    private readonly IReadOnlyRepository<Employee> _employeeRepository;

    public GetEmployeeByIdQueryHandler(IReadOnlyRepository<Employee> employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        Employee? employee = await _employeeRepository.GetFirstOrDefaultAsync
                                 (x => x.Id == request.Id, x => x.Include(c => c.Position), cancellationToken);

        if (employee is null)
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.Id} doesn't exist.");
        }

        return employee;
    }
}