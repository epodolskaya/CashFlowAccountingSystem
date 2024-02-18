using ApplicationCore.Entity;
using DomainServices.Features.OperationCategories.Queries.GetByDepartmentId;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.OperationCategories.Queries.GetByDepartmentId;

[TestClass]
public class GetOperationCategoriesByDepartmentIdQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<OperationCategory> result = await _mediator.Send(new GetOperationCategoriesByDepartmentIdQuery(1));

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidParameter()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>
            (async () => await _mediator.Send(new GetOperationCategoriesByDepartmentIdQuery(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetOperationCategoriesByDepartmentIdQuery(1), new CancellationToken(true)));
    }
}