using ApplicationCore.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Employees.Queries.GetByDepartmentId;
public class GetEmployeesByDepartmentIdQuery : IRequest<ICollection<Employee>>
{
    public GetEmployeesByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }

    public long DepartmentId { get; init; }
}
