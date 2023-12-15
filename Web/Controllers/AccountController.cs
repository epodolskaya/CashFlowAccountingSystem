using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Features.ChangePassword;
using Infrastructure.Identity.Features.Register;
using Infrastructure.Identity.Features.SignIn;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Web.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Login([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Policy = PolicyName.DepartmentHead)]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }
}