using FluentValidation;

namespace DomainServices.Features.Operations.Commands.Delete;

internal class DeleteOperationCommandValidator : AbstractValidator<DeleteOperationCommand>
{
    public DeleteOperationCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}