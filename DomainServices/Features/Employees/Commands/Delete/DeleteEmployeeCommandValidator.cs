using FluentValidation;

namespace DomainServices.Features.Employees.Commands.Delete;

internal class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}