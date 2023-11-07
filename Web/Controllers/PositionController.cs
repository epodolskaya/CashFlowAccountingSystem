using ApplicationCore.Entity;
using DomainServices.Features.Positions.Queries.GetAll;
using DomainServices.Features.Positions.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class PositionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<Position>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ICollection<Position>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllPositionsQuery query = new GetAllPositionsQuery();
        ICollection<Position> position = await _mediator.Send(query, cancellationToken);

        return Ok(position);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Position), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Position>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetPositionByIdQuery query = new GetPositionByIdQuery(id);
        Position position = await _mediator.Send(query, cancellationToken);

        return Ok(position);
    }
}