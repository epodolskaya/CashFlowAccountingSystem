using FluentValidation;

namespace DomainServices.Features.Employees.Queries.GetById;

internal class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}