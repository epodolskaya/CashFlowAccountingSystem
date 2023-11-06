using FluentValidation;

namespace DomainServices.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}