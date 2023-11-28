using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Employees.Queries.GetByDepartmentId;
public class GetEmployeesByDepartmentIdQueryValidator : AbstractValidator<GetEmployeesByDepartmentIdQuery>
{
    public GetEmployeesByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}
