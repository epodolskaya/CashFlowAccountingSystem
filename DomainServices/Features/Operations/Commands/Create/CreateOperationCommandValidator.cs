using FluentValidation;

namespace DomainServices.Features.Operations.Commands.Create;

internal class CreateOperationCommandValidator : AbstractValidator<CreateOperationCommand>
{
    private static readonly DateTime MinDateTime = DateTime.Parse("01.01.2023");

    public CreateOperationCommandValidator()
    {
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.CategoryId).GreaterThan(0);
        RuleFor(x => x.Date).GreaterThan(MinDateTime);
        RuleFor(x => x.Sum).GreaterThan(0);
    }
}