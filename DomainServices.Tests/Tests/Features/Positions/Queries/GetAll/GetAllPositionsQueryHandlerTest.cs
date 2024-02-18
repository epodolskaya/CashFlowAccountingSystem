using ApplicationCore.Entity;
using DomainServices.Features.Positions.Queries.GetAll;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Positions.Queries.GetAll;

[TestClass]
[TestSubject(typeof(GetAllPositionsQueryHandler))]
public class GetAllPositionsQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Position> result = await _mediator.Send(new GetAllPositionsQuery());

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllPositionsQuery(), new CancellationToken(true)));
    }
}