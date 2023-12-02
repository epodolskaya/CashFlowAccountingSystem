using FluentValidation;

namespace DomainServices.Features.Operations.Queries.GetByDepartmentId;

public class GetOperationsByDepartmentIdQueryValidator : AbstractValidator<GetOperationsByDepartmentIdQuery>
{
    public GetOperationsByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}