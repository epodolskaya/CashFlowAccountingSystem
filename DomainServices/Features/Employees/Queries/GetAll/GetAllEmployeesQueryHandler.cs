using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetAll;

internal class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ICollection<Employee>>
{
    private readonly IReadOnlyRepository<Employee> _employeesRepository;

    public GetAllEmployeesQueryHandler(IReadOnlyRepository<Employee> employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task<ICollection<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _employeesRepository.GetAllAsync(cancellationToken);
    }
}