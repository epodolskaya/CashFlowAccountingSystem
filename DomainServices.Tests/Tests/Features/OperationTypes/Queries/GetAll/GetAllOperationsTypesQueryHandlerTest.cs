using ApplicationCore.Entity;
using DomainServices.Features.OperationTypes.Queries.GetAll;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.OperationTypes.Queries.GetAll;

[TestClass]
public class GetAllOperationsTypesQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<OperationType> result = await _mediator.Send(new GetAllOperationsTypesQuery());

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllOperationsTypesQuery(), new CancellationToken(true)));
    }
}