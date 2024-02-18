using DomainServices.Features.Operations.Commands.Delete;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Commands.Delete;

[TestClass]
public class DeleteOperationCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        await _mediator.Send(new DeleteOperationCommand(3));
    }

    [TestMethod]
    public async Task InvalidId()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _mediator.Send(new DeleteOperationCommand(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new DeleteOperationCommand(1), new CancellationToken(true)));
    }
}