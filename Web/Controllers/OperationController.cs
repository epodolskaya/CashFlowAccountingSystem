using ApplicationCore.Entity;
using DomainServices.Features.Operations.Commands.Create;
using DomainServices.Features.Operations.Commands.Delete;
using DomainServices.Features.Operations.Commands.Update;
using DomainServices.Features.Operations.Queries.GetAll;
using DomainServices.Features.Operations.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class OperationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Operation>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllOperationsQuery query = new GetAllOperationsQuery();
        ICollection<Operation> operations = await _mediator.Send(query, cancellationToken);

        return Ok(operations);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Operation>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetOperationByIdQuery query = new GetOperationByIdQuery(id);
        Operation operation = await _mediator.Send(query, cancellationToken);

        return Ok(operation);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Operation>> Create([FromBody] CreateOperationCommand createCommand,
                                                      CancellationToken cancellationToken)
    {
        Operation createdOperation = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdOperation);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Operation>> Update([FromBody] UpdateOperationCommand updateCommand,
                                                      CancellationToken cancellationToken)
    {
        Operation updatedOperation = await _mediator.Send(updateCommand, cancellationToken);

        return Ok(updatedOperation);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteOperationCommand command = new DeleteOperationCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}