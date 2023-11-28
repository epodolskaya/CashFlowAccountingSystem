using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;
public class GetOperationCategoriesByDepartmentIdQueryValidator : AbstractValidator<GetOperationCategoriesByDepartmentIdQuery>
{
    public GetOperationCategoriesByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}
