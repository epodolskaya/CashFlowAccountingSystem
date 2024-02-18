using DomainServices.Features.Employees.Commands.Delete;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Employees.Commands.Delete;

[TestClass]
public class DeleteEmployeeCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        await _mediator.Send(new DeleteEmployeeCommand(3));
    }

    [TestMethod]
    public async Task InvalidId()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _mediator.Send(new DeleteEmployeeCommand(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new DeleteEmployeeCommand(1), new CancellationToken(true)));
    }
}