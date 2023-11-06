using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetAll;

public class GetAllEmployeesQuery : IRequest<ICollection<Employee>> { }