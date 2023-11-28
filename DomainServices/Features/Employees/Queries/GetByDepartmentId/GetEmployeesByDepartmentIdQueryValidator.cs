using FluentValidation;

namespace DomainServices.Features.Employees.Queries.GetByDepartmentId;

public class GetEmployeesByDepartmentIdQueryValidator : AbstractValidator<GetEmployeesByDepartmentIdQuery>
{
    public GetEmployeesByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}