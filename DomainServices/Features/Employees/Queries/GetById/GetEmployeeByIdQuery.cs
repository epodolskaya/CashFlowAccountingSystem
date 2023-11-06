using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetById;

internal class GetEmployeeByIdQuery : IRequest<Employee>
{
    public long Id { get; set; }
}