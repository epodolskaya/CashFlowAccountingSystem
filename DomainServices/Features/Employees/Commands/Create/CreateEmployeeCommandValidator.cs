using FluentValidation;

namespace DomainServices.Features.Employees.Commands.Create;

internal class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.DateOfBirth).GreaterThanOrEqualTo(DateTime.Now.AddYears(-14));
        RuleFor(x => x.PositionId).GreaterThan(0);
        RuleFor(x => x.PhoneNumber).Length(13);
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
    }
}