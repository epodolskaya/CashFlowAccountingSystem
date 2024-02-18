using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using DomainServices.Features.Operations.Commands.Update;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Commands.Update;

[TestClass]
public class UpdateOperationCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        UpdateOperationCommand command = new UpdateOperationCommand
        {
            Id = 1,
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
        UpdateOperationCommand command = new UpdateOperationCommand
        {
            Id = 0,
            DepartmentId = 0,
            CategoryId = 0,
            Comment = "Комментарий",
            Date = DateTime.Now,
            Sum = -1
        };

        ValidationException exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                            (async () => await _mediator.Send(command));

        Assert.AreEqual(4, exception.Errors.Count());
    }

    [TestMethod]
    public async Task UpdateNotExistingEntity()
    {
        UpdateOperationCommand command = new UpdateOperationCommand
        {
            Id = long.MaxValue,
            DepartmentId = 1,
            CategoryId = 1,
            Comment = "Комментарий",
            Date = DateTime.Now,
            Sum = 100
        };

        await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await _mediator.Send(command));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        UpdateOperationCommand command = new UpdateOperationCommand
        {
            Id = 1,
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