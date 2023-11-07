using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetAll;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ICollection<Employee>>
{
    private readonly IReadOnlyRepository<Employee> _employeesRepository;

    public GetAllEmployeesQueryHandler(IReadOnlyRepository<Employee> employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<ICollection<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return _employeesRepository.GetAllAsync(cancellationToken);
    }
}