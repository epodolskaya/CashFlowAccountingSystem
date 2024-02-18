using ApplicationCore.Entity;
using DomainServices.Features.OperationCategories.Queries.GetAll;
using DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;
using DomainServices.Features.OperationCategories.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Policy = PolicyName.DepartmentHead)]
public class OperationCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<OperationCategory>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOperationCategoriesQuery query = new GetAllOperationCategoriesQuery();
        ICollection<OperationCategory> categories = await _mediator.Send(query, cancellationToken);

        return Ok(categories);
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<ICollection<OperationCategory>>> GetByDepartmentId([FromRoute] long departmentId,
        CancellationToken cancellationToken)
    {
        GetOperationCategoriesByDepartmentIdQuery query = new GetOperationCategoriesByDepartmentIdQuery(departmentId);
        ICollection<OperationCategory> operationCategories = await _mediator.Send(query, cancellationToken);

        return Ok(operationCategories);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<OperationCategory>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationCategoryByIdQuery query = new GetOperationCategoryByIdQuery(id);
        OperationCategory category = await _mediator.Send(query, cancellationToken);

        return Ok(category);
    }
}