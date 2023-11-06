using ApplicationCore.Entity;
using DomainServices.Features.OperationCategories.Queries.GetAll;
using DomainServices.Features.OperationCategories.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class OperationCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<OperationCategory>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ICollection<OperationCategory>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOperationCategoriesQuery query = new GetAllOperationCategoriesQuery();
        ICollection<OperationCategory> categories = await _mediator.Send(query, cancellationToken);

        return Ok(categories);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OperationCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<OperationCategory>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationCategoryByIdQuery query = new GetOperationCategoryByIdQuery(id);
        OperationCategory category = await _mediator.Send(query, cancellationToken);

        return Ok(category);
    }
}