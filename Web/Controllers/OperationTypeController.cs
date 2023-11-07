using ApplicationCore.Entity;
using DomainServices.Features.OperationTypes.Queries.GetAll;
using DomainServices.Features.OperationTypes.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class OperationTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<OperationType>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ICollection<OperationType>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOperationsTypesQuery query = new GetAllOperationsTypesQuery();
        ICollection<OperationType> types = await _mediator.Send(query, cancellationToken);

        return Ok(types);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OperationType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<OperationType>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationTypeByIdQuery query = new GetOperationTypeByIdQuery(id);
        OperationType types = await _mediator.Send(query, cancellationToken);

        return Ok(types);
    }
}