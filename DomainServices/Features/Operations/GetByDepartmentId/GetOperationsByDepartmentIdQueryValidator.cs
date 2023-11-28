using FluentValidation;

namespace DomainServices.Features.Operations.GetByDepartmentId;

public class GetOperationsByDepartmentIdQueryValidator : AbstractValidator<GetOperationsByDepartmentIdQuery>
{
    public GetOperationsByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}