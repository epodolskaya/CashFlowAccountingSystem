using FluentValidation;

namespace DomainServices.Features.Operations.Commands.Update;

public class UpdateOperationCommandValidator : AbstractValidator<UpdateOperationCommand>
{
    private static readonly DateTime MinDateTime = DateTime.Parse("01.01.2023");

    public UpdateOperationCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
        RuleFor(x => x.Date).GreaterThan(MinDateTime);
        RuleFor(x => x.Date).LessThan(DateTime.Now);
        RuleFor(x => x.Sum).GreaterThan(0);
    }
}