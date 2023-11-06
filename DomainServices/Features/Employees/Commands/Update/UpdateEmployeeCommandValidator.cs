using FluentValidation;

namespace DomainServices.Features.Employees.Commands.Update;

internal class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(DateTime.Now.AddYears(-14));
        RuleFor(x => x.PositionId).GreaterThan(0);
        RuleFor(x => x.PhoneNumber).Length(13);
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
    }
}