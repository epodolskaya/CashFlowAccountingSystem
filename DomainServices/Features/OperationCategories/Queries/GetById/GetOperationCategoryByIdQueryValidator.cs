using FluentValidation;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

internal class GetOperationCategoryByIdQueryValidator : AbstractValidator<GetOperationCategoryByIdQuery>
{
    public GetOperationCategoryByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}