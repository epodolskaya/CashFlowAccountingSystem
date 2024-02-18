using ApplicationCore.Entity;
using DomainServices.Features.Positions.Queries.GetById;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Positions.Queries.GetById;

[TestClass]
[TestSubject(typeof(GetPositionByIdQueryHandler))]
public class GetPositionByIdQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        Position result = await _mediator.Send(new GetPositionByIdQuery(1));

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidParameter()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>(async () => await _mediator.Send(new GetPositionByIdQuery(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetPositionByIdQuery(1), new CancellationToken(true)));
    }
}