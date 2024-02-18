using ApplicationCore.Entity;
using DomainServices.Features.Operations.Commands.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Commands.Create;

[TestClass]
public class CreateOperationCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        CreateOperationCommand command = new CreateOperationCommand
        {
            DepartmentId = 1,
            CategoryId = 1,
            Comment = "Комментарий",
            Date = DateTime.Now,
            Sum = 100
        };

        Operation result = await _mediator.Send(command);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidRequest()
    {
        CreateOperationCommand command = new CreateOperationCommand
        {
            DepartmentId = 0,
            CategoryId = 0,
            Comment = null,
            Date = DateTime.MaxValue,
            Sum = -1
        };

        ValidationException exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                            (async () => await _mediator.Send(command));

        Assert.AreEqual(4, exception.Errors.Count());
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        CreateOperationCommand command = new CreateOperationCommand
        {
            DepartmentId = 1,
            CategoryId = 1,
            Comment = "Комментарий",
            Date = DateTime.Now,
            Sum = 100
        };

        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(command, new CancellationToken(true)));
    }
}