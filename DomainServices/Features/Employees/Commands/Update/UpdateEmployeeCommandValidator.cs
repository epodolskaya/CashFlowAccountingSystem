using ApplicationCore.Constants;
using FluentValidation;

namespace DomainServices.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-14));
        RuleFor(x => x.PositionId).GreaterThan(0);
        RuleFor(x => x.PhoneNumber).Matches(RegularExpressions.PhoneNumber);
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DepartmentId).GreaterThanOrEqualTo(0);
    }
}