using ApplicationCore.Entity;
using DomainServices.Features.Departments.Queries.GetAll;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Department>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllDepartmentsQuery query = new GetAllDepartmentsQuery();
        ICollection<Department> departments = await _mediator.Send(query, cancellationToken);

        return Ok(departments);
    }
}