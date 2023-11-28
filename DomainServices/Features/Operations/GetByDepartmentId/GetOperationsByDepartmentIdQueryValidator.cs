using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Operations.GetByDepartmentId;
public class GetOperationsByDepartmentIdQueryValidator : AbstractValidator<GetOperationsByDepartmentIdQuery>
{
    public GetOperationsByDepartmentIdQueryValidator()
    {
        RuleFor(x => x.DepartmentId).GreaterThan(0);
    }
}
