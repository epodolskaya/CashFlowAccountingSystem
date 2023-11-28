using FluentValidation;

namespace DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;

public class GetOperationCategoriesByDepartmentIdQueryValidator : AbstractValidator<GetOperationCategoriesByDepartmentIdQuery>
{
    public GetOperationCategoriesByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}