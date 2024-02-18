using ApplicationCore.Entity;
using DomainServices.Features.Operations.Queries.GetAll;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Queries.GetAll;

[TestClass]
public class GetAllOperationsQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Operation> result = await _mediator.Send(new GetAllOperationsQuery());

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllOperationsQuery(), new CancellationToken(true)));
    }
}