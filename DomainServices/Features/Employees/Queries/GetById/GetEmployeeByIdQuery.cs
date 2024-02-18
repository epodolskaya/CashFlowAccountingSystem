using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQuery : IRequest<Employee>
{
    public GetEmployeeByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}