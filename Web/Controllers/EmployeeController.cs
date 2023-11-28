using ApplicationCore.Entity;
using DomainServices.Features.Employees.Commands.Create;
using DomainServices.Features.Employees.Commands.Delete;
using DomainServices.Features.Employees.Commands.Update;
using DomainServices.Features.Employees.Queries.GetAll;
using DomainServices.Features.Employees.Queries.GetByDepartmentId;
using DomainServices.Features.Employees.Queries.GetById;
using Infrastructure.Identity.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(Policy = PolicyName.FinancialAnalyst)]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Employee>>> GetAll(CancellationToken cancellationToken)
    {
        GetAllEmployeesQuery query = new GetAllEmployeesQuery();
        ICollection<Employee> employees = await _mediator.Send(query, cancellationToken);

        return Ok(employees);
    }

    [HttpGet("{departmentId:long}")]
    public async Task<ActionResult<Employee>> GetByDepartmentId([FromRoute] long departmentId,
                                                                CancellationToken cancellationToken)
    {
        GetEmployeesByDepartmentIdQuery query = new GetEmployeesByDepartmentIdQuery(departmentId);
        ICollection<Employee> employees = await _mediator.Send(query, cancellationToken);

        return Ok(employees);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Employee>> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        GetEmployeeByIdQuery query = new GetEmployeeByIdQuery(id);
        Employee operation = await _mediator.Send(query, cancellationToken);

        return Ok(operation);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployeeCommand createCommand,
                                                     CancellationToken cancellationToken)
    {
        Employee createdEmployee = await _mediator.Send(createCommand, cancellationToken);

        return Ok(createdEmployee);
    }

    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Employee>> Update([FromBody] UpdateEmployeeCommand updateCommand,
                                                     CancellationToken cancellationToken)
    {
        Employee updatedEmployee = await _mediator.Send(updateCommand, cancellationToken);

        return Ok(updatedEmployee);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = PolicyName.FinancialAnalyst)]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        DeleteEmployeeCommand command = new DeleteEmployeeCommand(id);
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}