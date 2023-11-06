using FluentValidation;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

public class GetOperationCategoryByIdQueryValidator : AbstractValidator<GetOperationCategoryByIdQuery>
{
    public GetOperationCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}