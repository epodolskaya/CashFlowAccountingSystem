using ApplicationCore.Entity;
using DomainServices.Features.Operations.Queries.GetByDepartmentId;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Operations.Queries.GetByDepartmentId;

[TestClass]
public class GetOperationsByDepartmentIdQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Operation> result = await _mediator.Send(new GetOperationsByDepartmentIdQuery(1));

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidParameter()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>
            (async () => await _mediator.Send(new GetOperationsByDepartmentIdQuery(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetOperationsByDepartmentIdQuery(1), new CancellationToken(true)));
    }
}