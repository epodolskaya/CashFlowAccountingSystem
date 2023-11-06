using FluentValidation;

namespace DomainServices.Features.Operations.Queries.GetById;

internal class GetOperationByIdQueryValidator : AbstractValidator<GetOperationByIdQuery>
{
    public GetOperationByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}