using FluentValidation;

namespace DomainServices.Features.Operations.Commands.Create;

public class CreateOperationCommandValidator : AbstractValidator<CreateOperationCommand>
{
    private static readonly DateTime MinDateTime = DateTime.Parse("01.01.2023");

    public CreateOperationCommandValidator()
    {
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.TypeId).GreaterThan(0);
        RuleFor(x => x.DepartmentId).GreaterThan(0);
        RuleFor(x => x.Date).GreaterThan(MinDateTime);
        RuleFor(x => x.Sum).GreaterThan(0);
    }
}