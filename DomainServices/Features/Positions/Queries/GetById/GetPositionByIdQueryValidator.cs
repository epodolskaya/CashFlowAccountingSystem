using FluentValidation;

namespace DomainServices.Features.Positions.Queries.GetById;

internal class GetPositionByIdQueryValidator : AbstractValidator<GetPositionByIdQuery>
{
    public GetPositionByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}