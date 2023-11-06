using ApplicationCore.Entity;
using DomainServices.Features.Employees.Commands.Create;
using DomainServices.Features.Employees.Commands.Delete;
using DomainServices.Features.Employees.Commands.Update;
using DomainServices.Features.Employees.Queries.GetAll;
using DomainServices.Features.Employees.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ICollection<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<ICollection<Employee>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllEmployeesQuery query = new GetAllEmployeesQuery();
        ICollection<Employee> employees = await _mediator.Send(query, cancellationToken);

        return Ok(employees);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Employee>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetEmployeeByIdQuery query = new GetEmployeeByIdQuery(id);
        Employee operation = await _mediator.Send(query, cancellationToken);

        return Ok(operation);
    }

    [HttpPost]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployeeCommand createCommand,
                                                     CancellationToken cancellationToken)
    {
        Employee createdEmployee = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdEmployee);
    }

    [HttpPut]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Employee>> Update([FromBody] UpdateEmployeeCommand updateCommand,
                                                     CancellationToken cancellationToken)
    {
        Employee updatedEmployee = await _mediator.Send(updateCommand, cancellationToken);

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteEmployeeCommand command = new DeleteEmployeeCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}