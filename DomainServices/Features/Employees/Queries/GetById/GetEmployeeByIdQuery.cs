using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQuery : IRequest<Employee>
{
    public long Id { get; set; }

    public GetEmployeeByIdQuery(long id)
    {
        Id = id;
    }
}