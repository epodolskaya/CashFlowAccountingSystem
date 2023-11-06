using FluentValidation;

namespace DomainServices.Features.Positions.Queries.GetById;

public class GetPositionByIdQueryValidator : AbstractValidator<GetPositionByIdQuery>
{
    public GetPositionByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}