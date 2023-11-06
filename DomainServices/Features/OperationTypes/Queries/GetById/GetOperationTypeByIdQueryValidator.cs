using FluentValidation;

namespace DomainServices.Features.OperationTypes.Queries.GetById;

internal class GetOperationTypeByIdQueryValidator : AbstractValidator<GetOperationTypeByIdQuery>
{
    public GetOperationTypeByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}