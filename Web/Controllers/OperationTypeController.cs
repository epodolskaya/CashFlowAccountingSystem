using ApplicationCore.Entity;
using DomainServices.Features.Employees.Queries.GetByDepartmentId;
using DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;
using DomainServices.Features.Operations.GetByDepartmentId;
using DomainServices.Features.OperationTypes.Queries.GetAll;
using DomainServices.Features.OperationTypes.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class OperationTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<OperationType>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOperationsTypesQuery query = new GetAllOperationsTypesQuery();
        ICollection<OperationType> types = await _mediator.Send(query, cancellationToken);

        return Ok(types);
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<OperationCategory>> GetByDepartmentId([FromRoute] long departmentId, CancellationToken cancellationToken)
    {
        var query = new GetOperationCategoriesByDepartmentIdQuery(departmentId);
        var operationCategories = await _mediator.Send(query, cancellationToken);

        return Ok(operationCategories);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<OperationType>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationTypeByIdQuery query = new GetOperationTypeByIdQuery(id);
        OperationType types = await _mediator.Send(query, cancellationToken);

        return Ok(types);
    }
}