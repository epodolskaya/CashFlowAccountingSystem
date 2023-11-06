using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetAll;

internal class GetAllEmployeesQuery : IRequest<ICollection<Employee>> { }