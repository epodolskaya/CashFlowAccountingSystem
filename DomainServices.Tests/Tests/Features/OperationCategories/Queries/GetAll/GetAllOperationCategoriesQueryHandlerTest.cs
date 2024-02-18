using ApplicationCore.Entity;
using DomainServices.Features.OperationCategories.Queries.GetAll;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.OperationCategories.Queries.GetAll;

[TestClass]
public class GetAllOperationCategoriesQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<OperationCategory> result = await _mediator.Send(new GetAllOperationCategoriesQuery());

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllOperationCategoriesQuery(), new CancellationToken(true)));
    }
}