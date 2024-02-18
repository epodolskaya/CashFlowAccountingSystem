using ApplicationCore.Entity;
using DomainServices.Features.Operations.Queries.GetById;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Queries.GetById;

[TestClass]
public class GetOperationByIdQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        Operation result = await _mediator.Send(new GetOperationByIdQuery(1));

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidParameter()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _mediator.Send(new GetOperationByIdQuery(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetOperationByIdQuery(1), new CancellationToken(true)));
    }
}