using ApplicationCore.Entity;
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

    [HttpGet("{id:long}")]
    public async Task<ActionResult<OperationType>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationTypeByIdQuery query = new GetOperationTypeByIdQuery(id);
        OperationType types = await _mediator.Send(query, cancellationToken);

        return Ok(types);
    }
}