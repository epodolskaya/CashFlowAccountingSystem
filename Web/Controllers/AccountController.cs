﻿using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Features.Register;
using Infrastructure.Identity.Features.SignIn;
using Infrastructure.Identity.Features.SignOut;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> Login([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        await _mediator.Send(new SignOutCommand(), cancellationToken);
        return Ok();
    }
}