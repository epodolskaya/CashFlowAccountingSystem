using ApplicationCore.Entity;
using DomainServices.Features.Departments.Queries.GetAll;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Departments.Queries.GetAll;

[TestClass]
public class GetAllDepartmentsQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Department> result = await _mediator.Send(new GetAllDepartmentsQuery());
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllDepartmentsQuery(), new CancellationToken(true)));
    }
}